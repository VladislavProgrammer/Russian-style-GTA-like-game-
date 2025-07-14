using YG;
using UnityEngine;

public class CheckDeviceType : MonoBehaviour
{

    public static CheckDeviceType Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else Destroy(gameObject);
    }


    public bool IsMobileDevice()
    {
        string device = YandexGame.EnvironmentData.deviceType;
        Debug.Log("����������: " + device);

        if(device == "mobile" || device == "tablet")
        {
            return true;
        }

        else
        {
            return false;
        }

    }


    public bool IsRussianLanguage()
    {
        string language = YandexGame.EnvironmentData.language;
        

        if (language == "ru")
        {
            return true;
        }

        else
        {
            return false;
        }

    }
    


}
