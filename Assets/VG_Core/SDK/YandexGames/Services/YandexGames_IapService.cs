using System;
using System.Collections;
using System.Collections.Generic;
using VG.YandexGames;
using UnityEngine;


namespace VG
{
    public class YandexGames_IapService : IapService
    {
        private List<Iap.Product> _products;
        private List<string> _purchasedProductIds;
        private Dictionary<string, string> _productPrices;

        private string _currency = "YAN";


        public override bool supported =>
            Environment.platform == Environment.Platform.WebGL && !Environment.editor;


        public override void Consume(string key_product) => YG_Purchases.Consume(key_product);

        public override string GetPriceString(string key_product)
        {
            if (!_productPrices.ContainsKey(key_product))
                return string.Empty;

           return _productPrices[key_product].Replace("YAN", _currency);
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

        public override void InitializeProducts(List<Iap.Product> products)
        {
            _products = products;

            foreach (var product in products)
            {
                int productPurchaseQuantity = 0;

                foreach (var purchasedProductId in _purchasedProductIds)
                    if (product.key == purchasedProductId) productPurchaseQuantity++;

                product.Initialize(productPurchaseQuantity);
            }
        }

        public override void Purchase(string productKey, Action<bool> onSuccess)
            => YG_Purchases.Purchase(productKey, onSuccess);


        protected override void OnInitialized() { }

        public override void DeletePurchases()
        {
            foreach (var product in _products) product.ForceConsume();
        }


        public void SetPriceLanguage(Language language)
        {
            switch (language)
            {
                case Language.RU:
                    _currency = "ßÍÎÂ";
                    break;

                case Language.TR:
                case Language.EN:
                    _currency = "YAN";
                    break;

            }
        }



    }
}


