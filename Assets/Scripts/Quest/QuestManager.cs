using UnityEngine;
using TMPro;

[System.Serializable]
public class Quest
{
    public int ID;
    public string Name;
    public string Description;
    public bool isComplete;
    public Transform completePoint;

}


public class QuestManager : MonoBehaviour
{
    
    public Quest[] Quests;

    [SerializeField]
    public int currentQuestIndex;

    [SerializeField]
    private TextMeshProUGUI viewQuestName, viewDescriptionName;

    private void OnEnable()
    {
        EventManager.QuestCompleteEvent += QuestComplete;
    }

    private void OnDisable()
    {
        EventManager.QuestCompleteEvent -= QuestComplete;

    }

    private void Awake()
    {
        SetQuest();
    }

    void SetQuest()
    {
        Debug.Log("В работе квест: " + Quests[currentQuestIndex].ID);
        viewQuestName.text = Quests[currentQuestIndex].Name;
        viewDescriptionName.text = Quests[currentQuestIndex].Description;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J)) QuestComplete();
    }

    public void QuestComplete()
    {
        Quests[currentQuestIndex].isComplete = true;
        Debug.Log("Выполнен квест: " + Quests[currentQuestIndex].ID);
        NewQuest();
    }

    public void NewQuest()
    {
        if (currentQuestIndex < Quests.Length - 1)
        {
            if (Quests[currentQuestIndex].isComplete)
            {
                currentQuestIndex++;
                SetQuest();
            }

            else
            {
                Debug.Log("������� ����� �� ������� !");
            }
            
        }

        else Debug.Log("������ �����������");

    }


}
