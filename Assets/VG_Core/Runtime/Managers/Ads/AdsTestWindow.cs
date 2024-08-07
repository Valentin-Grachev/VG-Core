using UnityEngine;
using UnityEngine.UI;


namespace VG
{
    public class AdsTestWindow : MonoBehaviour
    {
        [SerializeField] private Button _interstitialButton;
        [SerializeField] private Button _rewardedButton;



        private void Start()
        {
            _interstitialButton.onClick.AddListener(() =>
            {
                Ads.Interstitial.Show();
            });

            _rewardedButton.onClick.AddListener(() =>
            {
                Ads.Rewarded.Show(onShown: (result) =>
                {
                    if (result == Ads.Rewarded.Result.Success)
                        Saves.Int[Key_Save.test_count].Value += 3;
                });
            });

        }





    }
}


