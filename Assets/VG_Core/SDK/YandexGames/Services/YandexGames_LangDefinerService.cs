using System.Collections;
using VG.YandexGames;

namespace VG
{
    public class YandexGames_LangDefinerService : LangDefinerService
    {
        public override bool supported =>
            Environment.platform == Environment.Platform.WebGL && !Environment.editor;


        private Language _browserLanguage;




        public override Language GetLanguage() => _browserLanguage;

        public override void Initialize()
        {
            StartCoroutine(RequestLanguage());
        }


        private IEnumerator RequestLanguage()
        {
            while (!YG_Sdk.available) yield return null;

            string language = YG_Sdk.GetLanguage();
            print("From unity: " + language);
            switch (language)
            {
                case "ru": _browserLanguage = Language.RU;
                    break;

                case "tr": _browserLanguage = Language.TR;
                    break;

                case "en":
                    _browserLanguage = Language.EN;
                    break;


                default: _browserLanguage = Language.EN;
                    break;
            }

            InitCompleted();
        }


    }
}



