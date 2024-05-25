using System.Collections.Generic;
using VG.Internal;

namespace VG
{
    public abstract class AnalyticsService : Service
    {

        public abstract void Track(string key_analytics, Dictionary<string, object> parameters);

    }
}



