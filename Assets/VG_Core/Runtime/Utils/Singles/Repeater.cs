using System;
using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    public class Repeater : Initializable
    {

        [System.Serializable]
        public class RepeatInfo
        {
            [SerializeField] private string _key; public string key => _key;
            [SerializeField] private float _timeInterval;
            public event Action onUpdate;

            private float _timeLeft = 0;


            public void SpendTime(float time)
            {
                _timeLeft -= time;
                if (_timeLeft < 0)
                {
                    _timeLeft = _timeInterval;
                    onUpdate?.Invoke();
                }
            }

        }


        [SerializeField] private List<RepeatInfo> _handlers;
        public static Dictionary<string, RepeatInfo> handlers = new Dictionary<string, RepeatInfo>();


        public override void Initialize()
        {
            foreach (var update in _handlers)
                handlers.Add(update.key, update);

            InitCompleted();
        }



        private void Update()
        {
            foreach (var update in _handlers)
                update.SpendTime(Time.unscaledDeltaTime);
        }

    }
}



