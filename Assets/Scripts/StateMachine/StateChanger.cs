using UnityEngine;

public class StateChanger : MonoBehaviour
{

    [SerializeField]
    private StaminaLogic staminaLogic;

    [SerializeField] 
    private PlayerMovement playerMovement;

    public bool CanDrive, CanShoot, PickedWeapon;

    private bool isShooting, isDriving;

    private bool isTalking;

    private void OnEnable()
    {
        EventManager.StartTalkEvent += StartTalkState;
        EventManager.StopTalkEvent += StopTalkState;
    }

    private void OnDisable()
    {
        EventManager.StartTalkEvent -= StartTalkState;
        EventManager.StopTalkEvent -= StopTalkState;
    }

    private void Update()
    {
        if (!isShooting && !isDriving)
        {
            CheckWalKableState();

        }

        CheckShootState();
        CheckDriveState();
    }

    void CheckWalKableState()
    {
        if ((InputManager.Instance.Horizontal != 0 || InputManager.Instance.Vertical != 0) && playerMovement.CanMove)
        {
            if (InputManager.Instance.PushRunButton)
            {
                if (staminaLogic.StaminaPoints > 0)
                {
                    StateMachine.Instance.SetState(StateMachine.State.Run);
                }

                else
                {
                    StateMachine.Instance.SetState(StateMachine.State.Tired);
                }
            }

            else
            {
                StateMachine.Instance.SetState(StateMachine.State.Walk);
            }
        }

        else
        {
            //if(!isShooting && !isDriving)
            StateMachine.Instance.SetState(StateMachine.State.Idle);
        }

        
    }

    void StartTalkState()
    {
        StateMachine.Instance.SetState(StateMachine.State.Talk);
        playerMovement.CanMove = false;
        isTalking = true;
    }

    void StopTalkState()
    {
        playerMovement.CanMove = true;
        isTalking = false;
    }

    void CheckShootState()
    {
        if (InputManager.Instance.PushFireButton)
        {
            if (CanShoot && !isTalking && !isDriving)
            {
                isShooting = true;
                StateMachine.Instance.SetState(StateMachine.State.Shoot);
            }
            
        }

        else isShooting = false;
    }

    void CheckDriveState()
    {
        if (InputManager.Instance.PushDriveButton)
        {
            if (CanDrive)
            {
                StateMachine.Instance.SetState(StateMachine.State.Drive);
                isDriving = true;
                if(PickedWeapon) CanShoot = false;
            }
        }

        if (InputManager.Instance.PushStopDrive)
        {
            if (isDriving)
            {
                StateMachine.Instance.SetState(StateMachine.State.Idle);
                isDriving = false;
                if(PickedWeapon) CanShoot = true;
            }
           
        }
    }

}
