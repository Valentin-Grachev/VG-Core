using System;
using System.Runtime.InteropServices;
using UnityEngine;



namespace VG.YandexGames
{
    public class YG_Saves : MonoBehaviour
    {
        
        public static bool available 
        { 
            get 
            {
#if UNITY_WEBGL
                CheckPlayerInit();
#endif
                return _playerInitialized;
            } 
        }


        public static void RequestSaves(Action<string> onSavesDataReceived)
        {
            _onSavesDataReceived = onSavesDataReceived;
#if UNITY_WEBGL
            RequestSaves();
#endif

        }

        public static void SendSaves(string savesData, Action<bool> onSavesSent)
        {
            _onSavesSent = onSavesSent;
#if UNITY_WEBGL
            SendSaves(savesData);
#endif

        }






        #region Internal

        private static bool _playerInitialized = false;

        
        private void HTML_OnPlayerInitChecked(int initialized) => _playerInitialized = Convert.ToBoolean(initialized);




        private static event Action<bool> _onSavesSent;
        
        private void HTML_OnSavesSent(int success) => _onSavesSent?.Invoke(Convert.ToBoolean(success));



        private static event Action<string> _onSavesDataReceived;
        
        private void HTML_OnSavesReceived(string savesData) => _onSavesDataReceived?.Invoke(savesData);


#endregion


#if UNITY_WEBGL
        [DllImport("__Internal")] private static extern void CheckPlayerInit();
        [DllImport("__Internal")] private static extern void SendSaves(string savesData);
        [DllImport("__Internal")] private static extern void RequestSaves();
        [DllImport("__Internal")] public static extern void InitializePlayer();

#endif


    }
}


