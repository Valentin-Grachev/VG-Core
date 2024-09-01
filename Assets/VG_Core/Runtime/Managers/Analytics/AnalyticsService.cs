using System.Collections.Generic;
using VG.Internal;

namespace VG
{
    public abstract class AnalyticsService : Service
    {

        public abstract void SendEvent(string eventKey, Dictionary<string, object> parameters);

    }
}



