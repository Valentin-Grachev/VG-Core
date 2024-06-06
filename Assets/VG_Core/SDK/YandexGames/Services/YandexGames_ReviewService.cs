using VG.YandexGames;
using System;



namespace VG
{
    public class YandexGames_ReviewService : ReviewService
    {
        public override bool supported =>
            Environment.platform == Environment.Platform.WebGL && !Environment.editor;


        public override void Initialize() => InitCompleted();


        public override void Request(Action onOpened, Action onClosed)
        {
            onOpened += () => TimeQueue.AddChange(TimeQueue.TimeType.StopAll);
            onClosed += () => TimeQueue.RemoveChange(TimeQueue.TimeType.StopAll);

            YG_Review.Request(onOpened, onClosed);
        }
    }
}


