using System;
using System.Runtime.InteropServices;
using UnityEngine;


namespace VG.YandexGames
{
    public class YG_Review : MonoBehaviour
    {
        private static Action _onOpened;
        private static Action _onClosed;



        public static void Request(Action onOpened, Action onClosed)
        {
            _onOpened = onOpened;
            _onClosed = onClosed;

#if UNITY_WEBGL
            RequestReview();
#endif
        }

#if UNITY_WEBGL
        [DllImport("__Internal")] private static extern void RequestReview();
#endif

        public void HTML_OnReviewOpened() => _onOpened?.Invoke();
        public void HTML_OnReviewClosed() => _onClosed?.Invoke();




    }
}


