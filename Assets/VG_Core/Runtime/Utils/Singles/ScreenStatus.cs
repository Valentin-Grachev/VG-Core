using System;
using UnityEngine;

namespace VG
{
    public class ScreenStatus : MonoBehaviour
    {
        public static event Action onResolutionChanged;


        private int _currentWidth;
        private int _currentHeight;

        public static float horizontalRatio => (float)Screen.width / (float)Screen.height;
        public static float verticalRatio => (float)Screen.height / (float)Screen.width;

        private void Awake()
        {
            _currentWidth = Screen.width;
            _currentHeight = Screen.height;
        }


        private void Update()
        {
            if (_currentHeight != Screen.height || _currentWidth != Screen.width)
            {
                _currentWidth = Screen.width;
                _currentHeight = Screen.height;
                onResolutionChanged?.Invoke();
            }
        }






    }
}



