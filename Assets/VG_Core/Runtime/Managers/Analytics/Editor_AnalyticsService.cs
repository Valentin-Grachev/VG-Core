using System.Collections.Generic;
using UnityEngine;
using VG.Internal;


namespace VG
{
    public class Editor_AnalyticsService : AnalyticsService
    {
        [SerializeField] private bool _useInBuild;


        public override bool supported => Environment.editor || _useInBuild;

        public override void Initialize() => InitCompleted();

        public override void Track(string key_analytics, Dictionary<string, object> parameters)
        {
            string message = "Event tracked: " + key_analytics;
            foreach (var parameter in parameters)
                message += "\n" + parameter.Key + ": " + parameter.Value.ToString();

            Core.LogEditor(message);
        }
    }
}


