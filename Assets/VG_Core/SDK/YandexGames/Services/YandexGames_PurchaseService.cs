using System;
using System.Collections;
using System.Collections.Generic;
using VG.YandexGames;
using UnityEngine;


namespace VG
{
    public class YandexGames_PurchaseService : PurchaseService
    {
        [SerializeField] private ProductCatalog _productCatalog;

        private List<string> _purchasedProductIds;
        private Dictionary<string, string> _productPrices;



        public override bool supported =>
            Environment.platform == Environment.Platform.WebGL && !Environment.editor;


        public override string GetPriceString(string productKey)
        {
            if (!_productPrices.ContainsKey(productKey))
                return string.Empty;

           return _productPrices[productKey];
        }




        public override void Initialize()
        {
            Saves.onDeleted += OnSavesDeleted;
            StartCoroutine(PurchasesInitializing());
        }

        

        private IEnumerator PurchasesInitializing()
        {
            yield return new WaitUntil(() => YG_Purchases.available);

#if UNITY_WEBGL
                YG_Purchases.InitializePayments();
#endif

            YG_Purchases.GetPurchasedProducts((purchasedProductIds) =>
            {
                _purchasedProductIds = purchasedProductIds;
                foreach (var purchasedProductKey in _purchasedProductIds)
                {
                    PurchasesHandler.HandlePurchase(purchasedProductKey);
                    if (_productCatalog.GetProduct(purchasedProductKey).consumable)
                        YG_Purchases.Consume(purchasedProductKey);
                }
            });

            YG_Purchases.GetPrices((productPrices) 
                => _productPrices = productPrices);

            yield return new WaitUntil(
                () => _purchasedProductIds != null && _productPrices != null);

            InitCompleted();
        }

        public override void Purchase(string productKey, Action<bool> onSuccess)
        {
            onSuccess += (success) =>
            {
                if (success) YG_Purchases.Consume(productKey);
            };
            YG_Purchases.Purchase(productKey, onSuccess);
        }


        private void OnSavesDeleted()
        {
            YG_Purchases.GetPurchasedProducts((purchasedProductIds) =>
            {
                foreach (var purchasedProductKey in purchasedProductIds)
                    YG_Purchases.Consume(purchasedProductKey);
            });
        }


    }
}


