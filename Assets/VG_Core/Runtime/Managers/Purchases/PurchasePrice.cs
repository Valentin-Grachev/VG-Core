using TMPro;
using UnityEngine;


namespace VG
{
    public class PurchasePrice : MonoBehaviour
    {
        [SerializeField] private ProductContainer _product;
 

        private void Start()
        {
            GetComponent<TextMeshProUGUI>().text = Purchases.GetPriceString(_product.id);
        }



    }
}



