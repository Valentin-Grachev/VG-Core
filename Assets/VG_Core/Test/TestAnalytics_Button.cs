using UnityEngine;
using VG;
using System.Collections.Generic;


public class TestAnalytics_Button : ButtonHandler
{
    private enum TestType { Event, Counter}

    [SerializeField] private TestType _testType;


    protected override void OnClick()
    {
        switch (_testType)
        {
            case TestType.Event:
                Analytics.SendEvent(Key_Event.test_event);
                break;

            case TestType.Counter:

                Analytics.SendEvent(Key_Event.test_counter.eventName, 
                    new Dictionary<string, object>
                    {
                        { Key_Event.test_counter.value, Saves.Int[Key_Save.test_count].Value } 
                    });

                break;
        }

    }
    
}