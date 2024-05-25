using System.Collections.Generic;
using UnityEngine;

namespace VG
{
    public class AudioStack : MonoBehaviour
    {
        private List<AudioSource> _audioSources = new List<AudioSource>();


        public AudioSource GetAudioSource()
        {
            for (int i = 0; i < _audioSources.Count; i++)
                if (!_audioSources[i].isPlaying) return _audioSources[i];

            var newAudioSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(newAudioSource);
            return newAudioSource;
        }




    }
}



