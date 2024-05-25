using UnityEngine;
using System.Collections.Generic;
using NaughtyAttributes;


namespace VG
{
    [CreateAssetMenu(menuName = "VG/" + nameof(SoundUnit), fileName = nameof(SoundUnit))]
    public class SoundUnit : ScriptableObject
    {
        public enum PlayType { Simple, Loop, OneShot }


        [SerializeField] private Sound.Channel _channel; public Sound.Channel channel => _channel;
        [SerializeField] private PlayType _playType; public PlayType playType => _playType;


        [HideIf(nameof(UseRandomAudioClip))]
        [SerializeField] private AudioClip _audioClip;
        [ShowIf(nameof(UseRandomAudioClip))]
        [SerializeField] private List<AudioClip> _randomAudioClips;

        [HideIf(nameof(UseRandomVolume))]
        [SerializeField] [Range(0f, 1f)] private float _volume = 1f;
        [ShowIf(nameof(UseRandomVolume))]
        [MinMaxSlider(0f, 1f)] [SerializeField] private Vector2 _randomVolume;


        [HideIf(nameof(UseRandomPitch))]
        [SerializeField] [Range(-3f, 3f)] private float _pitch = 1f;
        [ShowIf(nameof(UseRandomPitch))]
        [MinMaxSlider(-3f, 3f)] [SerializeField] private Vector2 _randomPitch;

        [Space(10)]
        [SerializeField] private bool _useRandomAudioClip;
        [SerializeField] private bool _useRandomVolume;
        [SerializeField] private bool _useRandomPitch;


        private bool UseRandomAudioClip() => _useRandomAudioClip;
        private bool UseRandomVolume() => _useRandomVolume;
        private bool UseRandomPitch() => _useRandomPitch;



        public AudioClip audioClip => _useRandomAudioClip ? 
            _randomAudioClips[Random.Range(0, _randomAudioClips.Count)] : _audioClip;

        public float volume => _useRandomVolume ?
            Random.Range(_randomVolume.x, _randomVolume.y) : _volume;

        public float pitch => _useRandomPitch ?
            Random.Range(_randomPitch.x, _randomPitch.y) : _pitch;



    }
}


