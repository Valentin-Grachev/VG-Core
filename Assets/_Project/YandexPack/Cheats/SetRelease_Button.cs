using UnityEngine;
using VG;

public class SetRelease_Button : ButtonHandler
{
    private int click = 8;


    protected override void OnClick()
    {
        click--;
        if (click == 0)
        {
            Saves.Delete();

            Hack.SetRelease();
            button.image.color = Color.yellow;
        }
        
    }

}
