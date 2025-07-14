using UnityEngine;
using TMPro;
using YG;

public class TranslateUI : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI currentText;

    [SerializeField, TextArea(5, 1)]
    string ru;

    [SerializeField, TextArea(5, 1)]
    string en;

    [SerializeField]
    CheckDeviceType checkDeviceType;

    private void Awake()
    {
        currentText = GetComponent<TextMeshProUGUI>();
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
