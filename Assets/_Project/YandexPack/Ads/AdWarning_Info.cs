using UnityEngine;
using VG;

public class AdWarning_Info : Info
{
    [SerializeField] private GameObject _warning;

    protected override void Subscribe()
    {
        Saves.Bool[Key_Save.ads_enabled].onChanged += UpdateValue;
    }

    protected override void Unsubscribe()
    {
        Saves.Bool[Key_Save.ads_enabled].onChanged -= UpdateValue;
    }

    protected override void UpdateValue()
    {
        bool showWarning = Saves.Bool[Key_Save.ads_enabled].Value && !Hack.release;
        _warning.SetActive(showWarning);
    }
}
