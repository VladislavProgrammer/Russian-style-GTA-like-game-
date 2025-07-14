using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour, IMovable
{

    [SerializeField]
    private Transform mainCamera;

    // Movement stats

    public float Speed = 5f;
    
    [SerializeField]
    private float _rotateSpeed = 10f;

    private float _smoothTime;
    [SerializeField] private float _smoothVelocity;

    [SerializeField]
    CameraController cameraController;

    
    public bool CanMove = true;

    private CharacterController cc;


    private void OnEnable()
    {
        EventManager.StartTalkEvent += LockMove;
        EventManager.StopTalkEvent += UnLockMove;
    }

    private void OnDisable()
    {
        EventManager.StartTalkEvent -= LockMove;
        EventManager.StopTalkEvent -= UnLockMove;

    }

    void Start()
    {
        cc = GetComponent<CharacterController>();
    }

    void Update()
    {
        if(CanMove) TryMove();
        
    }

    

    public void TryMove()
    {
        Vector3 direction = new Vector3(InputManager.Instance.Horizontal, 0f, InputManager.Instance.Vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {

            float rotationAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref _smoothVelocity, _smoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            Vector3 move = Quaternion.Euler(0f, rotationAngle, 0f) * Vector3.forward;

            cc.Move(move.normalized * Speed * Time.deltaTime);
        }

    }

    void LockMove() => CanMove = false;
    
    void UnLockMove() => CanMove = true;

    public void Aiming()
    {
        Vector3 lookAim = new Vector3(0, cameraController._mouseX, 0);
        transform.rotation = Quaternion.Euler(lookAim);
    }
}
