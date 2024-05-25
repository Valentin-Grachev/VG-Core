using UnityEngine;

namespace VG
{
    [System.Serializable]
    public struct ProductContainer
    {
        [SerializeField] private ProductId _productId;
        [SerializeField] private bool _usePromotion;

        public string id => _productId.GetId(_usePromotion);

    }
}


