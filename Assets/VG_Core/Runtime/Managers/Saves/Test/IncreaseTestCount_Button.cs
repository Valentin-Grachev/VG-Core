using VG;


public class IncreaseTestCount_Button : ButtonHandler
{
    
    protected override void OnClick()
    {
        Saves.Int[Key_Save.test_count].Value++;
        Saves.Commit();
    }
    
}