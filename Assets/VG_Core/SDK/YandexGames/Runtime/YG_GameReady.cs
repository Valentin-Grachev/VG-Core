using System.Runtime.InteropServices;
using UnityEngine;


namespace VG.YandexGames
{
    public class YG_GameReady : MonoBehaviour
    {
        void OnEnable()
        {
#if UNITY_WEBGL
            if (!Environment.editor) GameReady();
#endif

        }

#if UNITY_WEBGL
        [DllImport("__Internal")] private static extern void GameReady();
#endif


    }


}
    
