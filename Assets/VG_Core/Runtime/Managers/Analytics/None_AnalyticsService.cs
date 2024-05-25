using System.Collections.Generic;
using UnityEngine;


namespace VG
{
    public class None_AnalyticsService : AnalyticsService
    {
        [SerializeField] private bool _useInBuild;

        public override bool supported => Environment.editor || _useInBuild;

        public override void Initialize() => InitCompleted();

        public override void Track(string key_analytics, Dictionary<string, object> parameters) { }

        protected override void OnInitialized() { }

    }
}


