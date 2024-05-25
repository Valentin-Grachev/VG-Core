using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    public class PurchasesMediator : Initializable
    {
        public override void Initialize() => Iap.onPurchased += OnPurchased;


        public void Handle(List<Iap.Product> products)
        {

            foreach (var product in products)
                for (int i = 0; i < product.purchasesQuantity; i++)
                    OnPurchased(product.key, true);


            Saves.Commit((success) =>
            {
                if (success)
                    foreach (var product in products)
                        product.HandleConsuming();
            });


        }

        protected override void OnInitialized() { }


        private void OnPurchased(string key_product, bool success)
        {
            if (success) PurchasesHandler.HandlePurchase(key_product);
        }
    }
}


