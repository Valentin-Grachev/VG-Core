using VG;
using UnityEngine;

public class SkipAds_Button : ButtonHandler
{
    private int click = 8;

    protected override void OnClick()
    {
        click--;
        if (click == 0)
        {
            Saves.Bool[Key_Save.ads_enabled].Value = false;
            button.image.color = Color.green;
        }

    }
}
