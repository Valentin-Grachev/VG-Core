using VG;

public class HackedAdIcon_Info : Info
{
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
        bool showIcon = !Hack.release && Saves.Bool[Key_Save.ads_enabled].Value;
        gameObject.SetActive(showIcon);
    }


}
