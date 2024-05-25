using System.Collections.Generic;
using UnityEngine;


namespace VG
{

    [CreateAssetMenu(menuName = "VG/Localization/Tokens", fileName = "Tokens")]
    public class TokenData : ScriptableObject
    {
        [SerializeField] private List<string> _tokenKeys;

        public Dictionary<string, string> tokens { get; private set; }


        public void Initialize()
        {
            tokens = new Dictionary<string, string>();
            foreach (var tokenKey in _tokenKeys)
                tokens.Add(tokenKey, string.Empty);
        }


    }
}



