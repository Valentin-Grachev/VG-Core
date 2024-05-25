using UnityEngine;
using UnityEngine.UI;


namespace VG
{
    [RequireComponent(typeof(Button))]
    public abstract class ButtonHandler : MonoBehaviour
    {
        private Button _button;


        public Button button 
        { 
            get
            {
                _button ??= GetComponent<Button>();
                return _button;
            }
        }


        protected virtual void Start()
        {
            _button = GetComponent<Button>();
            _button.onClick.AddListener(OnClick);
        }


        protected abstract void OnClick();

    }
}


