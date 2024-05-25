using TMPro;
using UnityEngine;


namespace VG
{
    public abstract class Info : MonoBehaviour
    {
        private void OnEnable()
        {
            Subscribe();
            UpdateValue();
        }

        private TextMeshProUGUI _text; protected TextMeshProUGUI text
        {
            get
            {
                _text ??= GetComponent<TextMeshProUGUI>();
                return _text;
            }
        }



        private void OnDisable() => Unsubscribe();

        private void OnDestroy() => Unsubscribe();


        protected abstract void Subscribe();

        protected abstract void Unsubscribe();

        protected abstract void UpdateValue();



    }
}


