using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class DialogueManager : MonoBehaviour
{
    public DialogueData currentDialogueData;

    [SerializeField] private TextMeshProUGUI textView;

    [HideInInspector] public int currentPhraseIndex;

    private string currentString;

    private AudioClip currentAudioClip;
        
    [SerializeField] private AudioSource _audioSource;

    [SerializeField] private float textSpeed = 0.01f;

    [SerializeField] private Button nextButton;
    
    [SerializeField] private GameObject dialogBG;

    [SerializeField] private Image profileIconView;
    
    private NPCAnimatorManager npcAnimatorManager;
    
    private bool  canNextPhrase;

    private bool dialogInProcess;

    public bool CanTalk;

    
    private void Awake() => dialogBG.SetActive(false);

    void Start()
    {
        npcAnimatorManager = GetComponent<NPCAnimatorManager>();

    }

    void SetCurrentProfileIcon()
    {
        profileIconView.sprite = currentDialogueData.personIcon;
    }
    
    void SetCurrentStringText()
    {
        currentString = LocalizationDialogue.Instance.GetLocalizedText(
            currentDialogueData.Phrases[currentPhraseIndex].textRu,
            currentDialogueData.Phrases[currentPhraseIndex].textEn);
    }

    void SetCurrentAudio()
    {
        currentAudioClip = LocalizationDialogue.Instance.GetLocalizedAudio(
            currentDialogueData.Phrases[currentPhraseIndex].audioRu,
            currentDialogueData.Phrases[currentPhraseIndex].audioEn);
        PlayCurrentAudio();
    }

    void PlayCurrentAudio()
    {
        if (currentAudioClip != null && _audioSource != null)
        {
            _audioSource.Stop();
            _audioSource.clip = currentAudioClip;
            _audioSource.Play();
        }
    }

    // триггер -----
        
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            if (CanTalk)
            {
                Debug.Log("Начали диалог");
                StartDialog();
            }
            else Debug.Log("Не можем говорить");

        }
    }
    // триггер -----
    
    void StartDialog()
    {
        EventManager.CallStartTalkEvent();
        SetCurrentProfileIcon();
        npcAnimatorManager.SetDialogAnim();
        nextButton.onClick.AddListener(OnClickNextPhrase);
        dialogBG.SetActive(true);
        dialogInProcess = true;
        StartCoroutine(ShowText());
    }

    void EndDialog()
    {
        EventManager.CallStopTalkEvent();
        npcAnimatorManager.SetAnimState();
        dialogInProcess = false;
        CanTalk = false;
        dialogBG.SetActive(false); 
    }

    private IEnumerator ShowText()
    {
        if(currentPhraseIndex < currentDialogueData.Phrases.Length)
        {
            SetCurrentStringText();
            yield return  StartCoroutine(WriteWords(currentString));
            SetCurrentAudio();
            yield return new WaitUntil(() => canNextPhrase); // ждем нажатия кнопки
            currentPhraseIndex++;
            canNextPhrase = false;
            StartCoroutine(ShowText());
        }

        else
        {
            Debug.Log("����� ����������� ");
            EndDialog();
        }
       
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && dialogInProcess) 
        {
            
            OnClickNextPhrase();
        }
    }
    public void OnClickNextPhrase() => canNextPhrase = true;

    private IEnumerator WriteWords(string phrase)
    {
        textView.text = "";
        foreach(char letter in phrase.ToCharArray())
        {
            textView.text += letter;
            yield return new WaitForSeconds(textSpeed);
        }
    }
}
