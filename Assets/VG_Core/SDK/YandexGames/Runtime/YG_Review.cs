using System.Runtime.InteropServices;
using UnityEngine;


namespace VG.YandexGames
{
    public class YG_Review : MonoBehaviour
    {

        public static void Request()
        {
#if UNITY_WEBGL
            RequestReview();
#endif


        }

#if UNITY_WEBGL
        [DllImport("__Internal")] private static extern void RequestReview();
#endif



    }
}


