using System;
using System.Collections;
using System.Collections.Generic;
using VG.YandexGames;
using UnityEngine;


namespace VG
{
    public class YandexGames_IapService : IapService
    {
        private List<string> _purchasedProductIds;
        private Dictionary<string, string> _productPrices;



        public override bool supported =>
            Environment.platform == Environment.Platform.WebGL && !Environment.editor;


        public override void MarkAsConsumed(string key_product) => YG_Purchases.Consume(key_product);

        public override string GetPriceString(string key_product)
        {
            if (!_productPrices.ContainsKey(key_product))
                return string.Empty;

           return _productPrices[key_product];
        }




        public override void Initialize() => StartCoroutine(PurchasesInitializing());


        private IEnumerator PurchasesInitializing()
        {
            while (!YG_Purchases.available)
            {
                yield return new WaitForSecondsRealtime(0.1f);

#if UNITY_WEBGL
                YG_Purchases.InitializePayments();
#endif


            }

            YG_Purchases.GetPurchasedProducts((purchasedProductIds) => _purchasedProductIds = purchasedProductIds);

            YG_Purchases.GetPrices((productPrices) => _productPrices = productPrices);


            while (_purchasedProductIds == null || _productPrices == null) 
                yield return null;


            InitCompleted();
        }

        public override Dictionary<string, int> GetPurchasesQuantity()
        {
            var purchases = new Dictionary<string, int>();

            foreach (var purchasedProductId in _purchasedProductIds)
            {
                if (purchases.ContainsKey(purchasedProductId))
                    purchases[purchasedProductId]++;

                else purchases.Add(purchasedProductId, 1);
            }

            return purchases;
        }

        public override void Purchase(string productKey, Action<bool> onSuccess)
            => YG_Purchases.Purchase(productKey, onSuccess);



    }
}


