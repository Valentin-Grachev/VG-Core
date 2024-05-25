using UnityEngine;
using VG.Internal;

namespace VG
{
    [RequireComponent(typeof(PurchasesMediator))]
    public class Iap : Manager
    {
        [System.Serializable]
        public class Product
        {
            [SerializeField] private string _key; public string key => _key;
            [SerializeField] private bool _consumable; public bool consumable => _consumable;
            public int purchasesQuantity { get; private set; }

            public void Initialize(int purchasesQuantity)
                => this.purchasesQuantity = purchasesQuantity;

            public void HandleConsuming()
            {
                if (!_consumable) return;

                for (int i = 0; i < purchasesQuantity; i++) 
                    Consume(key);

                purchasesQuantity = 0;
            }

            public void ForceConsume()
            {
                for (int i = 0; i < purchasesQuantity; i++)
                    Consume(key);
            }
        }


        public delegate void OnPurchased(string key_product, bool success);
        public static event OnPurchased onPurchased;

        private static Iap instance;

        private static IapService service => instance.supportedService as IapService;
        protected override string managerName => "VG IAP";


        [SerializeField] private IapCatalog _iapCatalog;

        protected override void OnInitialized()
        {
            instance = this;

            service.InitializeProducts(_iapCatalog.products);

            var purchasesMediator = GetComponent<PurchasesMediator>();
            purchasesMediator.Initialize();
            purchasesMediator.Handle(_iapCatalog.products);

            Log(Core.Message.Initialized(managerName));
        }

        public static void DeletePurchases() => service.DeletePurchases();


        public static string GetPriceString(string key_product)
        {
            CheckProduct(key_product);
            return service.GetPriceString(key_product);
        }



        public static void Purchase(string key_product)
        {
            CheckProduct(key_product);

            instance.Log("Product purchase processing: " + key_product);

            service.Purchase(key_product, (success) =>
            {
                onPurchased?.Invoke(key_product, success);

                if (success)
                {
                    instance.Log("On purchased: " + key_product);

                    Saves.Commit((success) =>
                    {
                        if (success)
                            foreach (var product in instance._iapCatalog.products)
                                if (product.key == key_product)
                                {
                                    product.Initialize(1);
                                    product.HandleConsuming();
                                }
                    });

                }
                else instance.Log("On not purchased: " + key_product);
            });
        }



        private static void Consume(string key_product)
        {
            instance.Log("Consuming: " + key_product);
            service.Consume(key_product);
        }

        private static void CheckProduct(string key_product)
        {
            bool productExists = false;
            foreach (var product in instance._iapCatalog.products)
                if (product.key == key_product)
                {
                    productExists = true;
                    break;
                }

            if (!productExists) Core.Error.ProductDoesNotExists(instance.managerName, key_product);
        }


    }

}
