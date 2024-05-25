using UnityEngine;
using UnityEngine.Events;


namespace VG
{
    
    public class IapButton : ButtonHandler
    {
        [SerializeField] private ProductContainer _product;

        protected override void OnClick() => Iap.Purchase(_product.id);
    }
}



