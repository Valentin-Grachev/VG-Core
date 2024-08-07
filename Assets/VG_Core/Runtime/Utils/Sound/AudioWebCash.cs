using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using NaughtyAttributes;


namespace VG
{
    public class AudioWebCash : Initializable
    {
        public static bool available =>
            Environment.platform == Environment.Platform.WebGL;


        private static Dictionary<string, AudioClip> cashedClips = new Dictionary<string, AudioClip>();

        [SerializeField] private List<string> _cashedClipNames;

        private int _loadedClips = 0;


        public override void Initialize()
        {
            if (available) LoadAllClips();

            else InitCompleted();
        }

        public static AudioClip GetClip(string name) => cashedClips[name + ".mp3"];


        public void LoadAllClips()
        {
            if (_cashedClipNames.Count == 0 || _cashedClipNames == null)
                InitCompleted();

            cashedClips.Clear();
            _loadedClips = 0;

            foreach (var clipName in _cashedClipNames)
                StartCoroutine(LoadClip(clipName));
        }

        private IEnumerator LoadClip(string name)
        {
            string url = Application.streamingAssetsPath + "/" + name;

            UnityWebRequest request = UnityWebRequestMultimedia.GetAudioClip(url, AudioType.MPEG);
            request.SendWebRequest();
            yield return new WaitUntil(() => request.isDone);

            AudioClip audioClip = DownloadHandlerAudioClip.GetContent(request);
            
            cashedClips.Add(name, audioClip);
            _loadedClips++;

            if (_loadedClips == cashedClips.Count) InitCompleted();
        }

        [Button("Load cash names")]
        private void LoadCashNames()
        {
            _cashedClipNames = new List<string>();

            DirectoryInfo directory = new DirectoryInfo(Application.streamingAssetsPath);
            SearchFilesInsideDirectory(directory);
        }


        private void SearchFilesInsideDirectory(DirectoryInfo directory)
        {
            FileInfo[] info = directory.GetFiles("*.mp3");
            foreach (var item in info) _cashedClipNames.Add(item.Name);

            foreach (var insideDirectory in directory.GetDirectories())
                SearchFilesInsideDirectory(insideDirectory);
        }

    }
}


