using UnityEngine;

namespace VG
{
    [System.Serializable]
    public class Product
    {
        [SerializeField] private string _key; public string key => _key;
        [SerializeField] private bool _consumable; public bool consumable => _consumable;

        public void HandleConsuming(int purchasedQuantity)
        {
            if (purchasedQuantity == 0) return;

            if (!_consumable) PurchasesHandler.HandlePurchase(_key);
            else
            {
                for (int i = 0; i < purchasedQuantity; i++)
                {
                    PurchasesHandler.HandlePurchase(_key);
                    Iap.MarkAsConsumed(_key);
                }
            }
        }

        public void Delete(int purchasedQuantity)
        {
            for (int i = 0; i < purchasedQuantity; i++)
                Iap.MarkAsConsumed(_key);
        }
    }
}

    
