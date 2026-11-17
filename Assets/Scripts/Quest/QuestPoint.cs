using UnityEngine;

public class QuestPoint : MonoBehaviour, IInteractable
{
    [Header("Dialogue")] 
    [SerializeField] private string dialogueKnotName;
    
    [Header("Quest")]
    [SerializeField] private QuestInfo questInfo;

    [Header("Config")] 
    [SerializeField] private bool startPoint = true;
    [SerializeField] private bool endPoint = true;
    
    private bool playerIsNear = false;
    private string questId;
    private QuestState currentQuestState;
    
    private QuestIcon questIcon;

    private void Awake()
    {
        questId = questInfo.ID;
        questIcon = GetComponentInChildren<QuestIcon>();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange += QuestStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onQuestStateChange -= QuestStateChange;
    }
    
    public void Interact()
    {
        if (!playerIsNear)
            return;

        if (!dialogueKnotName.Equals(""))
            GameEventsManager.instance.dialogueEvents.onEnterDialogue?.Invoke(dialogueKnotName);
        else
        {
            if (currentQuestState == QuestState.CAN_START && startPoint)
                GameEventsManager.instance.questEvents.onStartQuest?.Invoke(questId);
            else if (currentQuestState == QuestState.CAN_FINISH && endPoint)
                GameEventsManager.instance.questEvents.onFinishQuest?.Invoke(questId);
        }
    }

    private void QuestStateChange(Quest quest)
    {
        if (!quest.info.ID.Equals(questId)) 
            return;
        
        currentQuestState = quest.state;
        questIcon.SetIconState(currentQuestState, startPoint, endPoint);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsNear = true;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            playerIsNear = false;
    }
}
