using System;

public class QuestEvents
{
    public Action<string> onStartQuest;
    public Action<string> onAdvanceQuest;
    public Action<string> onFinishQuest;
    public Action<Quest> onQuestStateChange;
    public Action<string, int, QuestStepState> onQuestStepChange;
}
