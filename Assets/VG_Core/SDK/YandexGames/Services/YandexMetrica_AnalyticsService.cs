using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using VG;
using VG.YandexGames;

public class YandexMetrica_AnalyticsService : AnalyticsService
{
    [SerializeField] private int _counterId;


    public override bool supported => 
        Environment.platform == Environment.Platform.WebGL && !Environment.editor;


    public override void Initialize() => InitCompleted();


    public override void SendEvent(string eventName, Dictionary<string, object> parameters)
    {
        var convertedParameters = new Dictionary<string, string>();

        foreach (var parameter in parameters)
            convertedParameters.Add(parameter.Key, parameter.Value.ToString());

        string jsonParameters = string.Empty;

        if (parameters != null && parameters.Count > 0)
            jsonParameters = ToJson(convertedParameters);

        YG_Analytics.SendEvent(_counterId, eventName, jsonParameters);
    }

    private string ToJson(IDictionary<string, string> dictionary)
    {
        var jsonString = "{";
        var kvpCount = 0;

        foreach (var kvp in dictionary)
        {
            if (string.IsNullOrEmpty(kvp.Key) || string.IsNullOrEmpty(kvp.Value)) continue;
            jsonString += $"\"{kvp.Key}\":{GetValueString(kvp.Value)},";
            kvpCount++;
        }

        if (kvpCount == 0) return string.Empty;

        if (dictionary.Count > 0)
            jsonString = jsonString.Remove(jsonString.Length - 1);

        jsonString += "}";

        return jsonString;
    }

    private string GetValueString(string value)
    {
        if (int.TryParse(value, out var intValue))
            return intValue.ToString();

        if (float.TryParse(value, out var floatValue))
            return floatValue.ToString(CultureInfo.InvariantCulture);

        if (bool.TryParse(value, out var boolValue))
            return boolValue.ToString().ToLower();

        value = value.Replace("\\", "\\\\").Replace("\"", "\\\"");
        return $"\"{value}\"";
    }



}
