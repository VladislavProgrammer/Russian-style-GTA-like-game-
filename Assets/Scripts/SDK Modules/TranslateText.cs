using UnityEngine;
using TMPro;
using YG;

public class TranslateText : MonoBehaviour
{
    [SerializeField]
    TextMeshPro currentText;

    [SerializeField, TextArea(5, 1)]
    string ru;

    [SerializeField, TextArea(5, 1)]
    string en;

    [SerializeField]
    CheckDeviceType checkDeviceType;

    private void Awake()
    {
        currentText = GetComponent<TextMeshPro>();
        CheckLanguage();
    }

    void CheckLanguage()
    {

        if (checkDeviceType.IsRussianLanguage())
        {
            currentText.text = ru;
        }

        else 
        {
            currentText.text = en;
        }
    }
}
