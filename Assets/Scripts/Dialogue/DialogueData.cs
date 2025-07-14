using UnityEngine;


[CreateAssetMenu(fileName = "DialogueData", menuName = "Game/DialogueData")]
public class DialogueData : ScriptableObject
{
    [System.Serializable]
    
    public class DialoguePhrase
    {
        [TextArea(3, 10)]
        public string textRu; // Текст на русском
        
        [TextArea(3, 10)]
        public string textEn; // Текст на английском

        public AudioClip audioRu; // Аудио на русском
        public AudioClip audioEn; // Аудио на английском
        
       
    }

    public DialoguePhrase[] Phrases; // Список фраз
    public Sprite personIcon; // Аватарка
}
