using UnityEngine;
using VG;

public class DeleteSaves_Button : ButtonHandler
{

    private int click = 8;

    protected override void OnClick()
    {
        click--;
        if (click == 0)
        {
            PlayerPrefs.DeleteAll();
            Saves.Delete();
            button.image.color = Color.red;
        }

    }
}
