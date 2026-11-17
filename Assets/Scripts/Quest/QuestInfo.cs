using UnityEngine;

[CreateAssetMenu(fileName = "QuestInfo", menuName = "Scriptable Objects/QuestInfo")]
public class QuestInfo : ScriptableObject
{
    [SerializeField] private string id;

    [Header("Quest Info")]
    [SerializeField] private string questName;
    [SerializeField] private string questDescription;
    
    [Header("Requirements")]
    [SerializeField] private int levelRequirement;
    [SerializeField] private QuestInfo[] questPrerequisites;
    
    [Header("Steps")]
    [SerializeField] private GameObject[] questStepsPref;
    
    [Header("Rewards")]
    public int goldReward;
    public int expReward;
    
    public string ID => id;

    public string QuestName => questName;

    public string QuestDescription => questDescription;

    public int LevelRequirement => levelRequirement;

    public QuestInfo[] QuestPrerequisites => questPrerequisites;

    public GameObject[] QuestStepsPref => questStepsPref;

    public int GoldReward => goldReward;

    public int ExpReward => expReward;

    private void OnValidate()
    {
        #if UNITY_EDITOR
        id = this.name;
        UnityEditor.EditorUtility.SetDirty(this); 
        #endif
    }
}
