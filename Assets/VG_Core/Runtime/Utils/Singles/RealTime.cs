using System;
using UnityEngine;

namespace VG
{
    public class RealTime : MonoBehaviour
    {
        public delegate void OnTimePassed(float seconds);
        public static event OnTimePassed onTimePassed;


        private void OnOneSecondPassed()
        {
            PassTime(1f);
        }

        private void Awake()
        {
            if (Startup.loaded) OnBootLoaded();
            else Startup.onLoaded += OnBootLoaded;
        }

        private void OnBootLoaded()
        {
            float passedSeconds = (float)(DateTime.Now - DateTime.Parse
            (Saves.String[Key_Save.last_time].Value)).TotalSeconds;

            PassTime(passedSeconds);
            Repeater.handlers[Key_Repeat.one_second].onUpdate += OnOneSecondPassed;
        }

        private void PassTime(float time)
        {
            if (time < 0f) time = 0f;

            Saves.String[Key_Save.last_time].Value = DateTime.Now.ToString();
            onTimePassed?.Invoke(time);
        }

    }
}


