using UnityEngine;
using UnityEngine.UI;

namespace VG
{
    [RequireComponent(typeof(Toggle))]
    public abstract class ToggleHandler : MonoBehaviour
    {
        public Toggle toggle { get; private set; }


        protected virtual void Start()
        {
            toggle = GetComponent<Toggle>();
            toggle.onValueChanged.AddListener(OnSwitch);
            toggle.isOn = startValue;
        }

        protected abstract bool startValue { get; }

        protected abstract void OnSwitch(bool value);
    }
}


