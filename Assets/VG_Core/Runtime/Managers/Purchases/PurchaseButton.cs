using UnityEngine;
using UnityEngine.Events;


namespace VG
{
    
    public class PurchaseButton : ButtonHandler
    {
        [SerializeField] private ProductContainer _product;

        protected override void OnClick() => Purchases.Purchase(_product.id);
    }
}



