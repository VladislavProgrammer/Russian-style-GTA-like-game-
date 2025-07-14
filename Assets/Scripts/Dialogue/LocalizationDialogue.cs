using UnityEngine;

public class LocalizationDialogue : MonoBehaviour
{
    public static LocalizationDialogue Instance { get; private set; }

    [SerializeField] private CheckDeviceType _checkDeviceType;
    public enum Language { Ru, En }
    public Language currentLanguage;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); 
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void  OnEnable()
    {
        if(_checkDeviceType.IsRussianLanguage()){
            currentLanguage = Language.Ru;
        } else currentLanguage = Language.En;
    }

    public string GetLocalizedText(string ruText, string enText)
    {
        return currentLanguage == Language.Ru ? ruText : enText;
    }

    public AudioClip GetLocalizedAudio(AudioClip ruAudio, AudioClip enAudio)
    {
        return currentLanguage == Language.Ru ? ruAudio : enAudio;
    }
}
