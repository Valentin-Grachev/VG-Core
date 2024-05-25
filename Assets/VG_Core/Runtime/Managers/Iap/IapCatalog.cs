using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    [CreateAssetMenu(menuName = "VG/Iap Catalog", fileName = "IAP Catalog")]
    public class IapCatalog : ScriptableObject
    {

        [SerializeField] private List<Iap.Product> _products;

        public List<Iap.Product> products { get => _products; }



    }
}



