using System.Collections.Generic;
using UnityEngine;
using VG;

public class YandexMetrica_AnalyticsService : AnalyticsService
{
    [SerializeField] private int _metricaCounterId;


    public override bool supported => 
        Environment.platform == Environment.Platform.WebGL && !Environment.editor;

    public override void Initialize()
    {
        throw new System.NotImplementedException();
    }

    public override void Track(string key_analytics, Dictionary<string, object> parameters)
    {
        throw new System.NotImplementedException();
    }
}
