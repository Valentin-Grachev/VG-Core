using TMPro;
using UnityEngine;

namespace VG
{
    public class FPS : MonoBehaviour
    {
        private int _frameCount = 0;
        private float _tickTimeLeft = 1f;
        [SerializeField] private TextMeshProUGUI _text;


        void Update()
        {
            _frameCount++;
            _tickTimeLeft -= Time.unscaledDeltaTime;

            if (_tickTimeLeft < 0)
            {
                _tickTimeLeft = 1f;
                _text.text = $"{_frameCount}";
                _frameCount = 0;
            }
        }
    }
}


