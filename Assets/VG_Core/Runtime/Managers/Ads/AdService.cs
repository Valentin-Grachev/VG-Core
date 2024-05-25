using System;
using VG.Internal;


namespace VG
{

    public abstract class AdService : Service
    {
        public abstract void ShowInterstitial(string key_ad, Action<bool> onSuccess);
        public abstract void ShowRewarded(string key_ad, Action<Ads.Rewarded.Result> onShown);
        public abstract void SetBanner(bool show);

    }
}
