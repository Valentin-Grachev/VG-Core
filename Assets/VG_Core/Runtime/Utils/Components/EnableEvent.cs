using UnityEngine;

namespace VG
{
    public class EnableEvent : MonoBehaviour
    {
        public delegate void OnChangeActive(bool active);
        public event OnChangeActive onChangeActive;

        private void OnEnable() => onChangeActive?.Invoke(true);

        private void OnDisable() => onChangeActive?.Invoke(false);


    }
}


