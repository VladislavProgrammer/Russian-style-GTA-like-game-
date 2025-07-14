using System;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action StartShootEvent;
    public static Action StopShootEvent;
    public static Action StartDriveEvent;
    public static Action StopDriveEvent;
    public static Action StartTalkEvent;
    public static Action StopTalkEvent;
    public static Action QuestCompleteEvent;
    public static void CallStartShootEvent() => StartShootEvent?.Invoke();
    public static void CallStopShootEvent() => StopShootEvent?.Invoke();
    public static void CallStartDriveEvent() => StartDriveEvent?.Invoke();
    public static void CallStopDriveEvent() => StopDriveEvent?.Invoke();
    public static void CallStartTalkEvent() => StartTalkEvent?.Invoke();
    public static void CallStopTalkEvent() => StopTalkEvent?.Invoke();
    
    public static void CallQuestCompleteEvent() => QuestCompleteEvent?.Invoke();

}
