using UnityEngine;
using VG;

public class ClosePurchaseWindow_Event : MonoBehaviour
{
    [SerializeField] private ProductContainer _product;


    private void OnEnable() => Purchases.onPurchased += OnProductPurchased;
    private void OnDisable() => Purchases.onPurchased -= OnProductPurchased;

    private void OnProductPurchased(string productKey, bool success)
    {
        if (!success) return;

        if (_product.id == productKey)
            gameObject.SetActive(false);
    }
}
