using UnityEngine;

namespace VG
{
    public class Environment : Initializable
    {
        public enum Platform { Android, WebGL, Desctop, iOS }

        public static bool test { get; private set; }

        [SerializeField] private bool _testVersion;


        public static bool editor
        {
            get
            {
                #if UNITY_EDITOR
                    return true;
                #else
                    return false;
                #endif
            }
        }

        public static Platform platform 
        { 
            get
            {
                #if UNITY_WEBGL
                    return Platform.WebGL;
                #endif
                
                #if UNITY_ANDROID
                    return Platform.Android;
                #endif
                
                #if UNITY_STANDALONE
                    return Platform.Desctop;
                #endif
                
                #if UNITY_IOS
                    return Platform.iOS;
                #endif

            }

        }

        public override void Initialize()
        {
            test = _testVersion;
            if (platform == Platform.Android) Application.targetFrameRate = 60;

            InitCompleted();
        }

    }
}



