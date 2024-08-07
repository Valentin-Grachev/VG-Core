using UnityEngine;
using NaughtyAttributes;
using System.Collections.Generic;

namespace VG
{

    public class ProductOrigin : ProductId
    {

        [Dropdown(nameof(GetAllProducts))]
        [SerializeField] private string _originProduct;

        [Dropdown(nameof(GetAllProducts))]
        [SerializeField] private string _promotionProduct;



        private List<string> GetAllProducts => Key_Product.all;


        public override string GetId(bool usePromotion)
            => usePromotion ? _promotionProduct : _originProduct;
    }
}
