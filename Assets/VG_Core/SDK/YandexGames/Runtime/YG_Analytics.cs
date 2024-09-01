using UnityEngine;

#if UNITY_WEBGL
using System.Runtime.InteropServices;
#endif


namespace VG.YandexGames
{
    public class YG_Analytics : MonoBehaviour
    {

        public static void SendEvent(int yandexMetricaCounterId, string eventName, string eventData)
        {
#if UNITY_WEBGL
            _SendEvent(yandexMetricaCounterId, eventName, eventData);
#endif
        }



#if UNITY_WEBGL

        [DllImport("__Internal")] private static extern void _SendEvent
            (int yandexMetricaCounterId, string eventName, string eventData);

#endif

    }

}



