using System;
using System.Runtime.InteropServices;
using UnityEngine;



namespace VG.YandexGames
{
    public class YG_Sdk : MonoBehaviour
    {



        private static bool _sdkInitialized = false;
        public static bool available 
        { 
            get 
            {
#if UNITY_WEBGL
                CheckSdkInit();
#endif
                return _sdkInitialized;
            } 
        }


        
        private void HTML_OnSdkInitChecked(int initialized) => _sdkInitialized = Convert.ToBoolean(initialized);



        public static string GetLanguage()
        {
#if UNITY_WEBGL
            RequestLanguage();
#endif
            return _receivedLanguage;
        }


        private static string _receivedLanguage;
        
        private void HTML_OnLanguageReceived(string language) => _receivedLanguage = language;



#if UNITY_WEBGL
        [DllImport("__Internal")] private static extern string RequestLanguage();
        [DllImport("__Internal")] private static extern void CheckSdkInit();
#endif

    }
}



