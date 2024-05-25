using UnityEngine;


namespace VG
{
    public abstract class Initializable : MonoBehaviour
    {
        public bool initialized { get; private set; }

        public abstract void Initialize();

        protected virtual void OnInitialized() { }

        protected void InitCompleted()
        {
            initialized = true;
            OnInitialized();
        }




    }
}


