using System;
using UnityEngine;

[Serializable]
public class QuestData
{
    public QuestState state;
    public int questStepId;
    public QuestStepState[] questStepStates;

    public QuestData(QuestState state, int questStepId, QuestStepState[] questStepStates)
    {
        this.state = state;
        this.questStepId = questStepId;
        this.questStepStates = questStepStates;
    }
}
