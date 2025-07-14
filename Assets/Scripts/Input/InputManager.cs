using UnityEngine;
using YG;


public class InputManager : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    public static InputManager Instance;

    [HideInInspector]
    public bool Move, PushRunButton, PushJumpButton, 
    PushFireButton, PushDriveButton, PushStopDrive;
    public float Horizontal { get; private set; }
    public float Vertical { get; private set; }

    private bool isMobile;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        GetDeviceType();
    }

    private void GetDeviceType()
    {
        isMobile = CheckDeviceType.Instance.IsMobileDevice();
    }

    void Update()
    {
        if (isMobile)
        {
            InputMobile();
        }

        else InputDesktop();
        
        CheckPushRunButton();
        CheckPushJumpButton();
        CheckPushFireButton();
        CheckPushDriveButton();
        CheckPushStopDriveButton();
        
        
    }


   

    void CheckPushRunButton()
    {
        if (!isMobile)
        {
            if (Input.GetKey(KeyCode.LeftShift)) { OnRunBTC(); }
            else OffRunBTC();
        }
        
    }

    void CheckPushJumpButton()
    {
        if (!isMobile)
        {
            if (Input.GetButtonDown("Jump")) { OnJumpBTC(); }
            else OffJumpBTC();
        }
        
    }

    void CheckPushFireButton()
    {
        if (!isMobile)
        {
            if (Input.GetMouseButton(0)) { OnFireBTC(); }
            else OffFireBTC();
        }
    }

    void CheckPushDriveButton()
    {
        if (!isMobile)
        {
            if (Input.GetKeyDown(KeyCode.R)) { OnDriveBTC(); }
            else OffDriveBTC();
        }
    }

    void CheckPushStopDriveButton()
    {
        if (!isMobile)
        {
            if (Input.GetKeyDown(KeyCode.F)) { OnStopDriveBTC(); }
            else OffStopDriveBTC();
        }
    }

    public void OnRunBTC() => PushRunButton = true; 
    public void OffRunBTC() => PushRunButton = false;
    public void OnJumpBTC() => PushJumpButton = true;
    public void OffJumpBTC() => PushJumpButton = false;
    public void OnFireBTC() => PushFireButton = true;
    public void OffFireBTC() => PushFireButton = false;
    public void OnDriveBTC() => PushDriveButton = true;
    public void OffDriveBTC() => PushDriveButton = false;
    public void OnStopDriveBTC() => PushStopDrive = true;
    public void OffStopDriveBTC() => PushStopDrive = false;


    void InputDesktop()
    {
        Horizontal = Input.GetAxis("Horizontal");
        Vertical = Input.GetAxis("Vertical");
        
    }
    void InputMobile()
    {
        Horizontal = joystick.Horizontal;
        Vertical = joystick.Vertical;
    }
}
