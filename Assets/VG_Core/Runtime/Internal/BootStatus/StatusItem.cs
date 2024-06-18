using TMPro;
using UnityEngine;

namespace VG.Internal
{
    public class StatusItem : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;


        public void SetName(string name)
        {
            this.name = name;
            _text.text = name;
        }
        

        public void SetStatus(bool initialized)
        {
            _text.color = initialized ? Color.green : Color.white;
        }
    }
}


