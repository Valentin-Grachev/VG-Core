using System.Collections;
using UnityEngine;
using VG.Internal;


namespace VG
{
    public class Purchases : Manager
    {
        public delegate void OnPurchased(string productKey, bool success);
        public static event OnPurchased onPurchased;

        private static Purchases instance;

        private static PurchaseService service => instance.supportedService as PurchaseService;
        protected override string managerName => "VG IAP";


        [SerializeField] private ProductCatalog _productCatalog;

        public override void Initialize()
        {
            StartCoroutine(InitializeWithSaves());
        }

        private IEnumerator InitializeWithSaves()
        {
            yield return new WaitUntil(() => Saves.Initialized);

            supportedService = GetSupportedService();
            supportedService.onInitialized += InitCompleted;
            supportedService.Initialize();
            
        }

        protected override void OnInitialized()
        {
            instance = this;
            Saves.Commit();
            Log(Core.Message.Initialized(managerName));
        }

        public static string GetPriceString(string productKey)
        {
            if (instance._productCatalog.ProductExists(productKey) == false)
                Core.Error.ProductDoesNotExists(instance.managerName, productKey);

            return service.GetPriceString(productKey);
        }



        public static void Purchase(string productKey)
        {
            if (instance._productCatalog.ProductExists(productKey) == false)
                Core.Error.ProductDoesNotExists(instance.managerName, productKey);

            instance.Log("Product purchase processing: " + productKey);

            service.Purchase(productKey, (success) =>
            {
                if (success)
                {
                    instance.Log("On purchased: " + productKey);
                    PurchasesHandler.HandlePurchase(productKey);
                    Saves.Commit();
                }
                else instance.Log("On not purchased: " + productKey);

                onPurchased?.Invoke(productKey, success);
            });
        }


    }

}
