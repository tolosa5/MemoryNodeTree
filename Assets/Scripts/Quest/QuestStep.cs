using UnityEngine;

public abstract class QuestStep : MonoBehaviour
{
    private bool isFinished = false;
    private string id;
    private int stepId;

    public void InitStep(string id, int stepId)
    {
        this.id = id;
        this.stepId = stepId;
    }

    protected void FinishQuestStep()
    {
        if (isFinished) 
            return;

        isFinished = true;
        GameEventsManager.instance.questEvents.onAdvanceQuest?.Invoke(id);
        Destroy(gameObject);
    }

    protected void ChangeState(string newState)
    {
        GameEventsManager.instance.questEvents.onQuestStepChange?.Invoke(id, stepId, new QuestStepState(newState));
    }
}
