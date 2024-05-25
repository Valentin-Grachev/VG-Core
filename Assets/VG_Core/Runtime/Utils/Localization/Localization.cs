using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    


    public class Localization : Initializable
    {
        private static Localization instance;
        public static event Action onChangeLanguage;
        public static event Action onUpdateToken;

        [SerializeField] private GoogleTables _googleTables;
        [Space(10)]
        [SerializeField] private Strings_LocalizedData _stringData;
        [SerializeField] private Sprites_LocalizedData _spriteData;
        [SerializeField] private TokenData _tokenData;
        [Space(10)]
        [SerializeField] private Language _defaultLanguage;
        [SerializeField] private List<Language> _supportedLanguages;


        public static Language currentLanguage { get; private set; }



        public static void SetLanguage(Language language)
        {
            Language selectedLanguage = instance._defaultLanguage;

            foreach (var availableLanguage in instance._supportedLanguages)
                if (language == availableLanguage)
                {
                    selectedLanguage = availableLanguage;
                    break;
                }


            currentLanguage = selectedLanguage;
            onChangeLanguage?.Invoke();
        }

        public override void Initialize()
        {
            instance = this;
            StartCoroutine(WaitTables());
        }


        private IEnumerator WaitTables()
        {
            while (!_googleTables.initialized && _googleTables.gameObject.activeInHierarchy)
                yield return null;

            _stringData.Initialize();
            _spriteData.Initialize();
            _tokenData.Initialize();

            InitCompleted();
        }

        public static string GetString(string key, bool useToken = false)
        {
            string localizedString = instance._stringData.translations[key].Get(currentLanguage);

            if (useToken)
                foreach (var token in instance._tokenData.tokens)
                {
                    string tokenDefinition = "{" + token.Key + "}";
                    if (localizedString.Contains(tokenDefinition))
                        localizedString = localizedString.Replace(tokenDefinition, token.Value);
                }

            return localizedString;
        }

        public static Sprite GetSprite(string key) 
            => instance._spriteData.translations[key].Get(currentLanguage);


        public static void SetToken(string key, string value)
        {
            instance._tokenData.tokens[key] = value;
            onUpdateToken?.Invoke();
        }

    }


}


