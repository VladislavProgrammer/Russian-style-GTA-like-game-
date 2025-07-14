using UnityEngine;

public class QuestPointChecker : MonoBehaviour
{
    [SerializeField]
    private QuestManager questManager;

    [SerializeField]
    private float minDistance;

    [SerializeField]
    private Transform arrowDirection;
    

    private void Update()
    {
        CheckDistance();
    }

    void CheckDistance()
    {
        Vector3 point = questManager.Quests[questManager.currentQuestIndex].completePoint.position;
        float distanceToPoint = Vector3.Distance(transform.position, point);
        arrowDirection.LookAt(point);

    }
}
