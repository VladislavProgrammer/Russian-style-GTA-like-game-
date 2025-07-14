using UnityEngine;

public class StateMachine : MonoBehaviour
{

    [System.Serializable]
    public enum State
    {
        Idle,
        Walk,
        Run,
        Tired,
        Shoot, 
        Drive,
        Talk
    }

    public State state;

    public static StateMachine Instance;

    [SerializeField] private PlayerAnimatorManager playerAnimatorManager;
    [SerializeField] private StaminaLogic staminaLogic;
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private ViewManager viewManager;
    [SerializeField] private SoundManager soundManager;
    public bool canRun = true;
    

    private void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(this.gameObject);
    }

    private void Start()
    {
        SetState(State.Idle);
    }


    public void SetState(State newState)
    {
        if (newState != state)
            ExitState(state); // 
        EnterState(newState);
    }



    void ExitState(State oldState)
    {
        switch (oldState)
        {
            case State.Idle:
                StopIdle();
                break;
            case State.Walk:
                StopWalk();
                break;
            case State.Run:
                StopRun();
                break;
            case State.Tired:
                StopTired();
                break;
            case State.Shoot:
                StopShoot();
                break;
            case State.Drive:
                StopDrive();
                break;
            case State.Talk:
                StopTalk();
                break;
        }
    }

    void EnterState(State newState)
    {
        switch (newState)
        {
            case State.Idle:
                PlayIdle();
                break;
            case State.Walk:
                PlayWalK();
                break;
            case State.Run:
                PlayRun();
                break;
            case State.Tired:
                PlayTired();
                break;
            case State.Shoot:
                PlayShoot();
                break;
            case State.Drive:
                PlayDrive();
                break;
            case State.Talk:
                PlayTalk();
                break;
        }

        state = newState;
    }




    void PlayIdle()
    {
        playerAnimatorManager.OffWalkAnim();
        Debug.Log("В состоянии покоя");
    }

    void StopIdle()
    {
        Debug.Log("Вышли из покоя");
    }

    void PlayWalK()
    {
        playerAnimatorManager.SetWalkAnim();
        soundManager.PlayWalkSound();
        Debug.Log("Начали ходить");

    }

    void StopWalk()
    {
        //playerAnimatorManager.OffWalkAnim();
        soundManager.StopWalkSound();
        Debug.Log("Закончили ходить");

    }

    void PlayRun()
    {
        if (canRun)
        {
            playerMovement.Speed = 8f;
            soundManager.PlayRunSound();
            playerAnimatorManager.SetRunAnim();
            Debug.Log("Начали бежать");
        }
    }

    void StopRun()
    {
        playerAnimatorManager.OffRunAnim();
        soundManager.StopRunSound();
        playerMovement.Speed = 5f;
        Debug.Log("Перестали бежать");
    }

    void PlayTired()
    {
        canRun = false;
        Invoke("NonTired", 2);
        playerMovement.Speed = 5f;
    }

    void StopTired()
    {
        Debug.Log("Вышли из усталости ");
    }

    void NonTired()
    {
        canRun = true;
    }

    void PlayShoot()
    {
        playerAnimatorManager.OnShootAnim();
        playerMovement.CanMove = false;
        playerMovement.Aiming();
        soundManager.PlayShootSound();
        Debug.Log("Начали стрелять ");
        EventManager.CallStartShootEvent();
    }

    void StopShoot()
    {
        playerAnimatorManager.OffShootAnim();
        soundManager.StopShootSound();
        playerMovement.CanMove = true;
        Debug.Log("Закончили стрелять");
        EventManager.CallStopShootEvent();
    }

    void PlayDrive()
    {
        Debug.Log("Начали водить !");
        EventManager.CallStartDriveEvent();
    }

    void StopDrive()
    {
        Debug.Log("Закончилм водить");
        EventManager.CallStopDriveEvent();
    }

    void PlayTalk()
    {
        Debug.Log("Начали говорить");
    }


    void StopTalk()
    {
        Debug.Log("Закончили говорить");
    }
}
