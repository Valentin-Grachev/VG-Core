using System;
using System.Collections;
using UnityEngine;
using VG.Internal;


namespace VG
{
    public class Ads : Manager
    {
        private static Ads instance; 

        public static bool skipAds 
        { 
            get => PlayerPrefs.GetInt(nameof(skipAds), 0) == 1;
            set
            {
                PlayerPrefs.SetInt(nameof(skipAds), Convert.ToInt32(value));
                Saves.Bool[Key_Save.ads_enabled].Value = !value;
                Saves.Commit();
            }
        }

        private static AdService service => instance.supportedService as AdService;
        protected override string managerName => "VG Ads";


        [SerializeField] private float _interstitialCooldown = 63f;

        private float _currentInterstitialCooldown = 0f;

        private static float interstitialCooldown => instance._interstitialCooldown;


        protected override void OnInitialized()
        {
            instance = this;
            _currentInterstitialCooldown = interstitialCooldown;
            Log(Core.Message.Initialized(managerName));
                
            StartCoroutine(SubscribeForEnableAds());
        }

        private IEnumerator SubscribeForEnableAds()
        {
            yield return new WaitUntil(() => Saves.Initialized);
            Saves.Bool[Key_Save.ads_enabled].onChanged += OnAdsEnabledChanged;
        }



        public static class Rewarded
        {
            public enum Result { Success, NotRewarded, NotAvailable }

            public delegate void OnShown(string key_ad, Result result);
            public static event OnShown onShown;
            

            public static void Show(string key_ad = "none", bool resetCooldown = false, Action<Result> onShown = null)
            {
                if (skipAds)
                {
                    onShown?.Invoke(Result.Success);
                    return;
                }


                instance.Log("Request rewarded ad. Ad key: " + key_ad);

                service.ShowRewarded(key_ad, (result) =>
                {
                    onShown?.Invoke(result);
                    Rewarded.onShown?.Invoke(key_ad, result);

                    if (result == Result.Success)
                    {
                        instance.Log("On rewarded. Ad key: " + key_ad);

                        if (resetCooldown)
                            instance._currentInterstitialCooldown = interstitialCooldown;
                    }
                    else instance.Log("On not rewarded. Ad key: " + key_ad);
                });
            }
        }



        public static class Interstitial
        {
            public enum Result { Success, Cooldown, NoAds, NotAvailable }

            public delegate void OnShown(string key_ad, Result result);
            public static event OnShown onShown;

            public static bool now => instance._currentInterstitialCooldown < 0f;

            public static void Show(string key_ad = "none", bool ignoreCooldown = false, Action<Result> onShown = null)
            {
                if (skipAds)
                {
                    onShown?.Invoke(Result.Success);
                    return;
                }


                instance.Log("Request interstitial ad. Ad key: " + key_ad);

                if (Saves.Bool[Key_Save.ads_enabled].Value == false)
                {
                    instance.Log("No Ads purchased. Interstitial rejected. Ad key: " + key_ad);
                    onShown?.Invoke(Result.NoAds);
                    Interstitial.onShown?.Invoke(key_ad, Result.NoAds);
                    return;
                }

                if (!now && !ignoreCooldown)
                {
                    instance.Log("Cooldown is not finished. Ad key: " + key_ad);
                    onShown?.Invoke(Result.Cooldown);
                    Interstitial.onShown?.Invoke(key_ad, Result.Cooldown);
                    return;
                }

                service.ShowInterstitial(key_ad, (success) =>
                {
                    Result result = success ? Result.Success : Result.NotAvailable;

                    onShown?.Invoke(result);
                    Interstitial.onShown?.Invoke(key_ad, result);

                    if (success)
                    {
                        instance.Log("On interstitial shown. Ad key: " + key_ad);
                        instance._currentInterstitialCooldown = interstitialCooldown;
                    }
                    else instance.Log("On interstitial failed. Ad key: " + key_ad);
                });
                return;
            }


        }

        public static class Banner
        {
            public static void Set(bool show)
            {
                if (Saves.Bool[Key_Save.ads_enabled].Value && show)
                    service.SetBanner(true);

                else service.SetBanner(false);
            }

        }

        private void OnAdsEnabledChanged()
        {
            if (Saves.Bool[Key_Save.ads_enabled].Value == false) 
                Banner.Set(false);
        }



        private void Update()
        {
            _currentInterstitialCooldown -= Time.unscaledDeltaTime;
        }



    }
}




