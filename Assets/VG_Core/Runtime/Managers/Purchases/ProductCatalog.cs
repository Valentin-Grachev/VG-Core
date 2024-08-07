using System.Collections.Generic;
using UnityEngine;
using VG.Internal;


namespace VG
{
    [CreateAssetMenu(menuName = "VG/Product Catalog", fileName = "Product Catalog")]
    public class ProductCatalog : ScriptableObject
    {
        [field: SerializeField] public List<Product> products { get; private set; }

        public bool ProductExists(string productKey)
        {
            foreach (var product in products)
                if (product.key == productKey) return true;

            return false;
        }


        public Product GetProduct(string productKey)
        {
            foreach (var product in products)
                if (product.key == productKey) return product;

            Core.Error.ProductDoesNotExists("VG Purchases", productKey);
            return null;
        }
    }
}



