using System;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    private Dictionary<string, Quest> questMap;
    
    private int currentPlayerLvl = 0;

    private void Awake()
    {
        questMap = CreateQuestMap();
    }

    private void OnEnable()
    {
        GameEventsManager.instance.questEvents.onStartQuest += StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest += AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest += FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepChange += QuestStepStateChange;
    }

    private void OnDisable()
    {
        GameEventsManager.instance.questEvents.onStartQuest -= StartQuest;
        GameEventsManager.instance.questEvents.onAdvanceQuest -= AdvanceQuest;
        GameEventsManager.instance.questEvents.onFinishQuest -= FinishQuest;
        GameEventsManager.instance.questEvents.onQuestStepChange -= QuestStepStateChange;
    }

    private void Start()
    {
        foreach (Quest quest in questMap.Values)
        {
            GameEventsManager.instance.questEvents.onQuestStateChange?.Invoke(quest);
        }
    }

    private void ChangeQuestState(string id, QuestState newState)
    {
        Quest quest = GetQuest(id);
        quest.state = newState;
        GameEventsManager.instance.questEvents.onQuestStateChange?.Invoke(quest);
    }

    private bool CheckRequirementsMet(Quest quest)
    {
        bool meetsRequirements = false;
        if (currentPlayerLvl >= quest.info.LevelRequirement)
            meetsRequirements = true;

        foreach (QuestInfo prerequisiteQuest in quest.info.QuestPrerequisites)
        {
            if (GetQuest(prerequisiteQuest.ID).state != QuestState.FINISHED)
                meetsRequirements = false;
        }
        
        return meetsRequirements;
    }

    private void Update()
    {
        foreach (Quest quest in questMap.Values)
        {
            if (quest.state == QuestState.REQUIREMENTS_NOT_MET && CheckRequirementsMet(quest))
                ChangeQuestState(quest.info.ID, QuestState.CAN_START);
        }
    }

    private void StartQuest(string id)
    {
        Quest quest = GetQuest(id);
        quest.CreateCurrentStep(transform);
        ChangeQuestState(id, QuestState.IN_PROGRESS);
    }

    private void AdvanceQuest(string id)
    {
        Quest quest = GetQuest(id);
        quest.MoveToNextStep();

        if (quest.CurrentStepExists())
            quest.CreateCurrentStep(transform);
        else
            ChangeQuestState(id, QuestState.CAN_FINISH);
    }

    private void FinishQuest(string id)
    {
        Quest quest = GetQuest(id);
        QuestRewards(quest);
        ChangeQuestState(id, QuestState.FINISHED);
    }

    private void QuestStepStateChange(string id, int stepId, QuestStepState newState)
    {
        Quest quest = GetQuest(id);
        quest.StoreCurrentStep(newState, stepId);
        ChangeQuestState(id, quest.state);
    }

    private void QuestRewards(Quest quest)
    {
        
    }

    private Dictionary<string, Quest> CreateQuestMap()
    {
        QuestInfo[] allQuests = Resources.LoadAll<QuestInfo>("Quests");
        Dictionary<string, Quest> idToQuestMap = new Dictionary<string, Quest>();
        foreach (QuestInfo questInfo in allQuests)
        {
            if (idToQuestMap.ContainsKey(questInfo.ID))
                Debug.LogWarning($"Quest {questInfo.ID} is already in use");
            else
                idToQuestMap.Add(questInfo.ID, new Quest(questInfo));
        }
        return idToQuestMap;
    }

    public Quest GetQuest(string id)
    {
        Quest quest = questMap[id];
        if (quest == null)
        {
            Debug.LogError("Quest not found");
            return null;
        }
        return quest;
    }

    private void OnApplicationQuit()
    {
        foreach (Quest quest in questMap.Values)
        {
            QuestData data = quest.GetQuestData();
        }
    }
}
