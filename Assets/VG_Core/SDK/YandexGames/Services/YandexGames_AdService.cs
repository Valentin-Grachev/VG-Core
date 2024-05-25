using System;
using System.Collections;
using VG.YandexGames;
using static VG.Ads.Rewarded;


namespace VG
{
    public class YandexGames_AdService : AdService
    {

        public override bool supported =>
            Environment.platform == Environment.Platform.WebGL && !Environment.editor;

        public override void Initialize()
        {
            StartCoroutine(WaitYandexSDK());
        }

        private IEnumerator WaitYandexSDK()
        {
            while (!YG_Sdk.available) yield return null;
            InitCompleted();
        }


        public override void SetBanner(bool show)
        {
#if UNITY_WEBGL
            if (show) YG_Ads.ShowBanner();
            else YG_Ads.HideBanner();
#endif

        }

        public override void ShowInterstitial(string key_ad, Action<bool> onSuccess)
        {
            bool interstitialWasShown = false;
            TimeQueue.AddChange(TimeQueue.TimeType.StopAll);

            YG_Ads.ShowInterstitial((action) =>
            {
                switch (action)
                {
                    case YG_Ads.InterstitialAction.Opened:
                        interstitialWasShown = true;
                        break;

                    case YG_Ads.InterstitialAction.Closed:
                        TimeQueue.RemoveChange(TimeQueue.TimeType.StopAll);
                        onSuccess?.Invoke(interstitialWasShown);
                        break;
                }

            });
        }

        public override void ShowRewarded(string key_ad, Action<Result> onShown)
        {
            bool rewarded = false;
            bool error = false;

            YG_Ads.ShowRewarded((action) =>
            {
                switch (action)
                {
                    case YG_Ads.RewardedAction.Opened:
                        TimeQueue.AddChange(TimeQueue.TimeType.StopAll);
                        break;

                    case YG_Ads.RewardedAction.Failed:
                        error = true;
                        break;

                    case YG_Ads.RewardedAction.Closed:
                        TimeQueue.RemoveChange(TimeQueue.TimeType.StopAll);

                        if (error) onShown?.Invoke(Result.NotAvailable);
                        else onShown?.Invoke(rewarded ? Result.Success : Result.NotRewarded);

                        break;

                    case YG_Ads.RewardedAction.Rewarded:
                        rewarded = true;
                        break;
                }

            });
        }
    }
}



