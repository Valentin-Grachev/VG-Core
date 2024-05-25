using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    [CreateAssetMenu(menuName = "VG/Iap Catalog", fileName = "IAP Catalog")]
    public class IapCatalog : ScriptableObject
    {

        [SerializeField] private List<Product> _products; public List<Product> products => _products; 

        private Dictionary<string, Product> _productsDictionary;

        public Product GetProduct(string key_product)
        {
            if (_productsDictionary == null)
            {
                _productsDictionary = new Dictionary<string, Product>();
                foreach (var item in _products)
                    _productsDictionary.Add(item.key, item);
            }

            if (_productsDictionary.ContainsKey(key_product))
                return _productsDictionary[key_product];

            return null;
        }



    }
}



