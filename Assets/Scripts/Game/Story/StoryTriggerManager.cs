using System;

public static class StoryTriggerManager
{
    public static event Action<int> StoryTriggered;

    public static void Trigger(int stage)
    {
        StoryTriggered?.Invoke(stage);
    }
}
