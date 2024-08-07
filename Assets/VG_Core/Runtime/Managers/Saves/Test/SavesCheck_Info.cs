using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace VG
{
    public class SavesCheck_Info : Info
    {
        [SerializeField] private Image _adsCheck;
        [SerializeField] private TextMeshProUGUI _testCountValue;


        protected override void Subscribe()
        {
            Saves.Bool[Key_Save.ads_enabled].onChanged += UpdateValue;
            Saves.Int[Key_Save.test_count].onChanged += UpdateValue;
        }

        protected override void Unsubscribe()
        {
            Saves.Bool[Key_Save.ads_enabled].onChanged -= UpdateValue;
            Saves.Int[Key_Save.test_count].onChanged -= UpdateValue;
        }

        protected override void UpdateValue()
        {
            _adsCheck.color = Saves.Bool[Key_Save.ads_enabled].Value ? 
                Color.green : Color.red;

            _testCountValue.text = $"—четчик = {Saves.Int[Key_Save.test_count].Value}";
        }


    }
}



