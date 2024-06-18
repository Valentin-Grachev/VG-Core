using TMPro;
using UnityEngine;

namespace VG.Internal
{
    public class VersionLabel : MonoBehaviour
    {
        private void OnEnable() 
            => GetComponent<TextMeshProUGUI>().text = Core.version;
    }
}


