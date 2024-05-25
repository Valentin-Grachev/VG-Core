using TMPro;
using UnityEngine;


namespace VG
{
    public class IapPrice : MonoBehaviour
    {
        [SerializeField] private ProductContainer _product;
 

        private void Start()
        {
            GetComponent<TextMeshProUGUI>().text = Iap.GetPriceString(_product.id);
        }



    }
}



