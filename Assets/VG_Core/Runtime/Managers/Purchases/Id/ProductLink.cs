using UnityEngine;


namespace VG
{
    public class ProductLink : ProductId
    {
        [SerializeField] private ProductId _productId;

        public override string GetId(bool usePromotion) => _productId.GetId(usePromotion);
    }
}


