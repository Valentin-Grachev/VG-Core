using UnityEngine;
using VG.Internal;

namespace VG
{
    public class Iap : Manager
    {
        public delegate void OnPurchased(string key_product, bool success);
        public static event OnPurchased onPurchased;

        private static Iap instance;

        private static IapService service => instance.supportedService as IapService;
        protected override string managerName => "VG IAP";


        [SerializeField] private IapCatalog _iapCatalog;

        protected override void OnInitialized()
        {
            instance = this;

            var purchasesQuantity = service.GetPurchasesQuantity();
            foreach (var purchase in purchasesQuantity)
                _iapCatalog.GetProduct(purchase.Key).HandleConsuming(purchase.Value);

            Saves.Commit();
            Log(Core.Message.Initialized(managerName));
        }

        public static void DeletePurchases()
        {
            var purchasesQuantity = service.GetPurchasesQuantity();
            foreach (var purchase in purchasesQuantity)
                instance._iapCatalog.GetProduct(purchase.Key).Delete(purchase.Value);
        }


        public static string GetPriceString(string key_product)
        {
            if (instance._iapCatalog.GetProduct(key_product) == null)
                Core.Error.ProductDoesNotExists(instance.managerName, key_product);

            return service.GetPriceString(key_product);
        }



        public static void Purchase(string key_product)
        {
            var product = instance._iapCatalog.GetProduct(key_product);
            if (product == null)
                Core.Error.ProductDoesNotExists(instance.managerName, key_product);

            instance.Log("Product purchase processing: " + key_product);

            service.Purchase(key_product, (success) =>
            {
                if (success)
                {
                    instance.Log("On purchased: " + key_product);
                    product.HandleConsuming(1);
                    Saves.Commit();
                }
                else instance.Log("On not purchased: " + key_product);

                onPurchased?.Invoke(key_product, success);
            });
        }



        public static void MarkAsConsumed(string key_product)
        {
            instance.Log("Mark as consumed: " + key_product);
            service.MarkAsConsumed(key_product);
        }



    }

}
