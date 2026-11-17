using UnityEngine;

public class Quest
{
    public QuestInfo info;

    public QuestState state;
    private int currentQuestStepId;
    private QuestStepState[] questStepStates;

    public Quest(QuestInfo info)
    {
        this.info = info;
        state = QuestState.REQUIREMENTS_NOT_MET;
        currentQuestStepId = 0;
        questStepStates = new QuestStepState[info.QuestStepsPref.Length];
        for (int i = 0; i < questStepStates.Length; i++)
        {
            questStepStates[i] = new QuestStepState();
        }
    }
    
    public void MoveToNextStep()
    {
        currentQuestStepId++;
    }
    
    public bool CurrentStepExists()
    {
        return currentQuestStepId < info.QuestStepsPref.Length;
    }
    
    public void CreateCurrentStep(Transform parentTransform)
    {
        GameObject questStepPref = GetCurrentStepPref();
        
        if (questStepPref != null)
            Object.Instantiate(questStepPref, parentTransform).
                GetComponent<QuestStep>().InitStep(info.ID, currentQuestStepId);
    }

    private GameObject GetCurrentStepPref()
    {
        GameObject questStepPref = null;

        if (CurrentStepExists())
            questStepPref = info.QuestStepsPref[currentQuestStepId];
        else
            Debug.LogError($"Quest {info.QuestName} doesn't exist");
        
        return questStepPref;
    }
    
    public void StoreCurrentStep(QuestStepState stepState, int id)
    {
        if (id < questStepStates.Length)
            questStepStates[id].state = stepState.state;
        else
            Debug.LogError($"Quest {info.QuestName} doesn't have a step with id {id}");
    }

    public QuestData GetQuestData()
    {
        return new QuestData(state, currentQuestStepId, questStepStates);
    }
}
