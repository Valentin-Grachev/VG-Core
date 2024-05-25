using UnityEngine;

namespace VG.Internal
{
    public class GlobalObject : MonoBehaviour
    {

        private void Awake()
        {
            transform.SetParent(null);
            DontDestroyOnLoad(gameObject);
        }
    }
}


