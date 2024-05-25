using System;
using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    public class UpdateHandler : Initializable
    {

        [System.Serializable]
        public class UpdateHandlerInfo
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


        [SerializeField] private List<UpdateHandlerInfo> _updates;
        public static Dictionary<string, UpdateHandlerInfo> updates = new Dictionary<string, UpdateHandlerInfo>();


        public override void Initialize()
        {
            foreach (var update in _updates)
                updates.Add(update.key, update);

            InitCompleted();
        }



        private void Update()
        {
            foreach (var update in _updates)
                update.SpendTime(Time.unscaledDeltaTime);
        }

    }
}



