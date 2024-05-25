using UnityEngine;
using UnityEngine.Events;


namespace VG
{
    public class LocalizedSprite : Info
    {
        [SerializeField] private string _key;
        [SerializeField] private UnityEvent<Sprite> _onUpdate;

        protected override void Subscribe()
        {
            Localization.onChangeLanguage += UpdateValue;
        }

        protected override void Unsubscribe()
        {
            Localization.onChangeLanguage -= UpdateValue;
        }

        protected override void UpdateValue() => _onUpdate?.Invoke(Localization.GetSprite(_key));
    }
}


