using UnityEngine;
using UnityEngine.UI;

public class StaminaLogic : MonoBehaviour
{

    [SerializeField]
    private Image _staminaBar;

    public float StaminaPoints = 100;

    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private PlayerAnimatorManager _playerAnimatorManager;

    [SerializeField]
    private float _runPrice = 0.5f, _regenerationValue = 1.5f;

    private bool canRun = true;

    private void Start()
    {
        SetView();
    }

    void SetView()
    {
        _staminaBar.fillAmount = StaminaPoints / 100;
    }

    public void ChangeValue()
    {
        if(StaminaPoints > 0)
        {
            StaminaPoints -= _runPrice;
            SetView();
        }
        

    }


    private void Update()
    {
        if (StateMachine.Instance.state == StateMachine.State.Run)
        {
            ChangeValue();
        }

        else Regeneration();    
    }

    void CheckState()
    {
        if (InputManager.Instance.Move)
        {

            if (InputManager.Instance.PushRunButton)
            {
                if (StaminaPoints > 0)
                {
                    canRun = true;
                    RunState();
                    Debug.Log("бег");
                   
                }

                else
                {
                    TiredState();
                    Debug.Log("Устал");
                }
            }

            else
            {
                WalkState();
                canRun = false;
                Debug.Log("Ходьба");
            }

           
            
        }

        else
        {
            IdleState();
            Debug.Log("покой");
        }

       

    }

    void RunState()
    {
        if (canRun)
        {
            ChangeValue();
            _playerMovement.Speed = 8f;
            _playerAnimatorManager.SetRunAnim();
        }
        
    }

    void WalkState()
    {
        Regeneration();
        _playerAnimatorManager.OffRunAnim();
        _playerMovement.Speed = 5f;
    }

    void IdleState()
    {
       Regeneration();
       _playerAnimatorManager.OffRunAnim();
       _playerMovement.Speed = 5f;
    }

    void TiredState()
    {
        canRun = false;
        Invoke("NonTired", 2);
        _playerAnimatorManager.OffRunAnim();
        _playerMovement.Speed = 5f;
    }

    void NonTired()
    {
        canRun = true;
    }

    public void Regeneration()
    {
        if(StaminaPoints < 100)
        {
            StaminaPoints += _regenerationValue;
            SetView();
        }
       
    }

}
