using UnityEngine;
using VG;

public class ShowNoAdsOffer_Event : EventHandler
{
    [SerializeField] private GameObject _offer;
    [SerializeField] private int _showEveryAd = 2;

    private int _currentShowsToOffer = 3;



    protected override void Subscribe()
    {
        Ads.Interstitial.onShown += OnInterstitialShown;
        Purchases.onPurchased += OnProductPurchased;
    }

    protected override void Unsubscribe()
    {
        Ads.Interstitial.onShown -= OnInterstitialShown;
        Purchases.onPurchased -= OnProductPurchased;
    }

    private void OnProductPurchased(string key_product, bool success)
    {
        if (success && key_product == Key_Product.no_ads)
            _offer.SetActive(false);
    }


    private void OnInterstitialShown(string key_ad, Ads.Interstitial.Result result)
    {
        if (result == Ads.Interstitial.Result.Success) 
            HandleOffer(key_ad);
    }

    private void HandleOffer(string adKey)
    {
        _currentShowsToOffer--;

        if (_currentShowsToOffer <= 0)
        {
            _offer.SetActive(true);
            _currentShowsToOffer = _showEveryAd;
        }
    }


    
}
