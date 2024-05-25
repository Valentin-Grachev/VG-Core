using TMPro;
using UnityEngine;


namespace VG
{
    [RequireComponent(typeof(TextMeshProUGUI))]
    public class LocalizedString : Info
    {
        [SerializeField] private string _key;
        [SerializeField] private bool _useToken;

        protected override void Subscribe()
        {
            Localization.onChangeLanguage += UpdateValue;
            if (_useToken) Localization.onUpdateToken += UpdateValue;
        }

        protected override void Unsubscribe()
        {
            Localization.onChangeLanguage -= UpdateValue;
            if (_useToken) Localization.onUpdateToken -= UpdateValue;
        }

        protected override void UpdateValue()
        {
            if (_key == string.Empty) return;
            text.text = Localization.GetString(_key, _useToken);
        }
        


        public void SetKey(string key, bool useToken)
        {
            _key = key;
            _useToken = useToken;
            UpdateValue();
        }


        private void OnValidate()
        {
            if (TryGetComponent<TextMeshProUGUI>(out var text)) text.text = $"<{_key}>";
        }




    }
}



