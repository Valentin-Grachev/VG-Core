using UnityEngine;


namespace VG
{
    public enum Language { RU, EN, TR }

    public interface ITranslation
    {
        public string Key { get; }
    }


    [System.Serializable]
    public class Token : ITranslation
    {
        [SerializeField] private string _key; public string Key => _key;
        [HideInInspector] public string value = null;
    }


    public class Translation<Type> : ITranslation
    {
        public string key; public string Key => key;
        [Space(10)]
        public Type ru;
        public Type en;
        public Type tr;

        public Type Get(Language language)
        {
            switch (language)
            {
                case Language.RU: return ru;
                case Language.EN: return en;
                case Language.TR: return tr;
            }

            throw new System.Exception("Wrong language");
        }
    }

    [System.Serializable] public class String_Translation : Translation<string> { }
    [System.Serializable] public class Sprite_Translation : Translation<Sprite> { }

}

