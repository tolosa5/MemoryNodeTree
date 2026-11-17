using UnityEngine;

public class QuestIcon : MonoBehaviour
{
    [Header("Icons")]
    [SerializeField] private GameObject cantStartIcon;
    [SerializeField] private GameObject canStartMetIcon;
    [SerializeField] private GameObject cantFinishIcon;
    [SerializeField] private GameObject canFinishIcon;

    public void SetIconState(QuestState state, bool isStartPoint, bool isFinishPoint)
    {
        cantStartIcon.SetActive(state == QuestState.REQUIREMENTS_NOT_MET && isStartPoint);
        canStartMetIcon.SetActive(state == QuestState.CAN_START && isStartPoint);
        cantFinishIcon.SetActive(state == QuestState.IN_PROGRESS && isFinishPoint);
        canFinishIcon.SetActive(state == QuestState.CAN_FINISH && isFinishPoint);
    }
}
