using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform targetPlayerPoint; // player  pos

    public Transform currentTarget;

    [SerializeField]
    private Vector2 _limitValue;

    [SerializeField]
    private float _sensvity;

    private Vector3 _lookInput;

    [HideInInspector]
    public float _mouseX, _mouseY;

    private bool isMobile;

    

    void Start()
    {
        isMobile = CheckDeviceType.Instance.IsMobileDevice();
        DefaultPoint();
    }

    private void OnEnable()
    {
        EventManager.StopDriveEvent += SetFollowPlayerPoint;
    }

    private void OnDisable()
    {
        EventManager.StopDriveEvent -= SetFollowPlayerPoint;
    }

    void LateUpdate()
    {
        if (isMobile)
        {
            LookMobile();
        }
        else LookDesktop();

        Looking();
    }

    void DefaultPoint()
    {
        SetFollowPlayerPoint();
    }

    void SetFollowPlayerPoint() => currentTarget = targetPlayerPoint;

    private void Looking()
    {
        if(currentTarget)
        transform.position = currentTarget.transform.position;

        _mouseY = Mathf.Clamp(_mouseY, _limitValue.x, _limitValue.y);

        _lookInput = new Vector3(-_mouseY, _mouseX, 0);

        transform.rotation = Quaternion.Euler(_lookInput);

    }

    private void LookDesktop()
    {
        _mouseX += Input.GetAxis("Mouse X") * _sensvity;
        _mouseY += Input.GetAxis("Mouse Y") * _sensvity;

    }

    private void LookMobile()
    {
        _mouseX += SimpleInput.GetAxis("PanelX") * _sensvity;
        _mouseY += SimpleInput.GetAxis("PanelY") * _sensvity;

    }

    

}
