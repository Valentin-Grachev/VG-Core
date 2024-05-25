using System;
using UnityEngine;
using UnityEngine.Audio;

namespace VG
{
    public static class SoundExtensions
    {
        public static AudioSource Play(this SoundUnit soundUnit) => Sound.Play(soundUnit);
    }



    public class Sound : Initializable
    {
        public enum Channel { Music, SFX }


        public class Settings
        {
            public static bool musicEnabled
            {
                get => Convert.ToBoolean(PlayerPrefs.GetInt("Music", 1));
                set
                {
                    float volume = value ? 0f : -80f;
                    instance._audioMixer.SetFloat("musicVolume", volume);
                    PlayerPrefs.SetInt("Music", value ? 1 : 0);
                }
            }

            public static bool sfxEnabled
            {
                get => Convert.ToBoolean(PlayerPrefs.GetInt("Sound", 1));
                set
                {
                    float volume = value ? 0f : -80f;
                    instance._audioMixer.SetFloat("sfxVolume", volume);
                    PlayerPrefs.SetInt("Sound", value ? 1 : 0);
                }
            }

            public static void Apply()
            {
                sfxEnabled = sfxEnabled;
                musicEnabled = musicEnabled;
            }
        }

        private static Sound instance;




        [SerializeField] private AudioMixer _audioMixer;
        [SerializeField] private AudioStack _sfxAudioStack;

        private AudioMixerGroup _sfxGroup;
        private AudioSource _musicAudioSource;


        public override void Initialize()
        {
            instance = this;

            _sfxGroup = _audioMixer.FindMatchingGroups("SFX")[0];

            _musicAudioSource = _sfxAudioStack.GetAudioSource();
            _musicAudioSource.outputAudioMixerGroup = _audioMixer.FindMatchingGroups("Music")[0];

            Settings.Apply();

            InitCompleted();
        }



        public static AudioSource Play(SoundUnit soundUnit)
        {
            AudioSource audioSource = soundUnit.channel == Channel.Music ? 
                instance._musicAudioSource : instance._sfxAudioStack.GetAudioSource();

            audioSource.clip = AudioWebCash.available ? 
                AudioWebCash.GetClip(soundUnit.audioClip.name) : soundUnit.audioClip;

            if (soundUnit.channel == Channel.SFX)
            audioSource.outputAudioMixerGroup = instance._sfxGroup;

            audioSource.volume = soundUnit.volume;
            audioSource.loop = soundUnit.playType == SoundUnit.PlayType.Loop;
            audioSource.pitch = soundUnit.pitch;

            if (soundUnit.playType == SoundUnit.PlayType.OneShot)
                audioSource.PlayOneShot(soundUnit.audioClip);
            else audioSource.Play();

            return audioSource;
        }

        
    }
}


