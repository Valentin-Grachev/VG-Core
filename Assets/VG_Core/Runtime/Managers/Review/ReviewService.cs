using System;
using VG.Internal;


namespace VG
{
    public abstract class ReviewService : Service
    {
        public abstract void Request(Action onOpened, Action onClosed);

    }
}

