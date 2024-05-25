using System;
using VG.Internal;
using UnityEngine;
using UnityEngine.UI;
using static VG.Ads.Rewarded;

namespace VG
{
    


    public class Test_AdService : AdService
    {
        [SerializeField] private bool _useInBuild;

        [Header("Rewarded:")]
        [SerializeField] private GameObject _rewardPanel;
        [SerializeField] private Button _acceptRewardButton;
        [SerializeField] private Button _notRewardedButton;
        [SerializeField] private Button _errorRewardedButton;

        [Header("Interstitial:")]
        [SerializeField] private GameObject _interPanel;
        [SerializeField] private Button _closeInterButton;
        [SerializeField] private Button _errorInterButton;


        public override bool supported => _useInBuild || Environment.editor;

        public override void Initialize() => InitCompleted();

        public override void SetBanner(bool show)
        {
            if (show) Core.LogEditor($"Show banner.");
            else Core.LogEditor($"Hide banner.");
        }


        public override void ShowInterstitial(string key_ad, Action<bool> onSuccess)
        {
            _interPanel.SetActive(true);

            _closeInterButton.onClick.RemoveAllListeners();
            _closeInterButton.onClick.AddListener(() =>
            {
                _interPanel.SetActive(false);
                onSuccess?.Invoke(true);
            });

            _errorInterButton.onClick.RemoveAllListeners();
            _errorInterButton.onClick.AddListener(() =>
            {
                _interPanel.SetActive(false);
                onSuccess?.Invoke(false);
            });

        }

        public override void ShowRewarded(string key_ad, Action<Result> onShown)
        {
            _rewardPanel.SetActive(true);

            _acceptRewardButton.onClick.RemoveAllListeners();
            _acceptRewardButton.onClick.AddListener(() =>
            {
                _rewardPanel.SetActive(false);
                onShown?.Invoke(Result.Success);
            });

            _notRewardedButton.onClick.RemoveAllListeners();
            _notRewardedButton.onClick.AddListener(() =>
            {
                _rewardPanel.SetActive(false);
                onShown?.Invoke(Result.NotRewarded);
            });

            _errorRewardedButton.onClick.RemoveAllListeners();
            _errorRewardedButton.onClick.AddListener(() =>
            {
                _rewardPanel.SetActive(false);
                onShown?.Invoke(Result.NotAvailable);
            });


        }

    }
}


