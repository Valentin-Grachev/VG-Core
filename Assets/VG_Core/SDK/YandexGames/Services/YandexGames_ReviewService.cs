using VG.YandexGames;



namespace VG
{
    public class YandexGames_ReviewService : ReviewService
    {
        public override bool supported =>
            Environment.platform == Environment.Platform.WebGL && !Environment.editor;


        public override void Initialize() => InitCompleted();


        public override void Request(System.Action onHandled)
        {
            YG_Review.Request();
            onHandled?.Invoke();
        }



    }
}


