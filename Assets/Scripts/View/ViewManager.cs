using UnityEngine;

public class ViewManager : MonoBehaviour
{
    [SerializeField] private GameObject mobileInputBg, playerObj, staminaBG;

    [SerializeField] private GameObject crossHairIcon;

    [SerializeField] private GameObject[] keyIcons;

    [SerializeField] private CheckDeviceType checkDeviceType;

    [SerializeField] private CurrentCarLogic currentCarLogic;

    [SerializeField] private GameObject buttonStopDrive, buttonStartDrive;

    [SerializeField] private GameObject playerInputPanel, carInputPanel, speedInfoPanel;
    
    private void OnEnable()
    {
        EventManager.StartShootEvent += ShowCrossHairIcon;
        EventManager.StopShootEvent += HideCrossHairIcon;
        EventManager.StartDriveEvent += StartDriveView;
        EventManager.StopDriveEvent += StopDriveView;

    }

    private void OnDisable()
    {
        EventManager.StartShootEvent -= ShowCrossHairIcon;
        EventManager.StopShootEvent -= HideCrossHairIcon;
        EventManager.StartDriveEvent -= StartDriveView;
        EventManager.StopDriveEvent -= StopDriveView;
     
    }

    private void Start() => SetCurrentView();

    private void Awake()
    {
        TryHideCursor();
        HideAllIconsBtc();
        SetCurrentIcons();
    }

    void HideAllIconsBtc()
    {
        HideCrossHairIcon();
        HideButtonStopDrive();
        HideButtonStartDrive();
        HideMobileCarInputs();
    }


    void SetCurrentView()
    {
        if (checkDeviceType.IsMobileDevice())
        {
            mobileInputBg.SetActive(true);
        }

        else mobileInputBg.SetActive(false);
    }


    void SetCurrentIcons()
    {
        if (checkDeviceType.IsMobileDevice())
        {
            foreach (var icon in keyIcons)
            {
                icon.SetActive(false);
            }
        }
        else
        {
            foreach (var icon in keyIcons)
            {
                icon.SetActive(true);
            }
        }

    }

    void ShowCrossHairIcon() => crossHairIcon.SetActive(true);
    void HideCrossHairIcon() => crossHairIcon.SetActive(false);

    void StartDriveView()
    {
        DeactivePlayer();
        ShowButtonStopDrive();
        HideButtonStartDrive();
        TryShowMobileCarInputs();

    }

    void StopDriveView()
    {
        ActivePlayer();
        HideButtonStopDrive();
        TryHideMobileCarInputs();
    }
    
    void DeactivePlayer()
    {
        Transform carTransform = currentCarLogic.currentCarController.GetComponent<Transform>();
        playerObj.transform.SetParent(carTransform);
        staminaBG.SetActive(false);
        playerObj.gameObject.SetActive(false); 
    }

    void ActivePlayer()
    {
        playerObj.gameObject.SetActive(true);
        staminaBG.SetActive(true);
        playerObj.transform.SetParent(null);
       
    }

    void TryHideCursor()
    {
        if (!checkDeviceType.IsMobileDevice())
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
        }
        
    }

    void TryShowMobileCarInputs()
    {
       if(speedInfoPanel) speedInfoPanel.SetActive(true);
        
        if (checkDeviceType.IsMobileDevice())
        {
           if(playerInputPanel) playerInputPanel.SetActive(false);
           if(carInputPanel) carInputPanel.SetActive(true);
        }
    }

    void TryHideMobileCarInputs()
    {
       if(speedInfoPanel) speedInfoPanel.SetActive(false);
        
        if (checkDeviceType.IsMobileDevice())
        {
            if(carInputPanel) carInputPanel.SetActive(false);
            if(playerInputPanel) playerInputPanel.SetActive(true);
        }
    }

    void ShowButtonStopDrive() => buttonStopDrive.transform.localScale = new Vector3(1, 1, 1);
    void HideButtonStopDrive() => buttonStopDrive.transform.localScale = new Vector3(0, 0, 0);

    private void HideMobileCarInputs()
    {
        carInputPanel.SetActive(false);
        speedInfoPanel.SetActive(false);
    }

    public void ShowButtonStartDrive() => buttonStartDrive.transform.localScale = new Vector3(1, 1, 1);
    public void HideButtonStartDrive() => buttonStartDrive.transform.localScale = new Vector3(0, 0, 0);
    

}
