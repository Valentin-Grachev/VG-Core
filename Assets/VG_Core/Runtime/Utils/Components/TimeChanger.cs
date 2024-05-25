using UnityEngine;


namespace VG
{


    public class TimeChanger : MonoBehaviour
    {
        [SerializeField] private TimeQueue.TimeType _timeType;


        private bool _enabled = false;

        private void OnEnable()
        {
            if (!_enabled) Enable();
        }


        private void OnDisable()
        {
            if (_enabled) Disable();
        }


        private void Enable()
        {
            _enabled = true;
            TimeQueue.AddChange(_timeType);
        }



        private void Disable()
        {
            _enabled = false;
            TimeQueue.RemoveChange(_timeType);
        }



    }
}


