using UnityEngine;

public class IdleState : IPlayerState
{
    private readonly PlayerController _playerController;

    public IdleState(PlayerController playerController)
    {
        _playerController = playerController;
    }

    public void Enter()
    {
        PlayerAnimatorManager.Instance.SetIdleAnim();
        Debug.Log("В состоянии покоя");
    }

    public void Update()
    {
        TryWalk();
    }

    void TryWalk()
    {
        Vector3 direction = new Vector3(InputManager.Instance.Horizontal, 0f, InputManager.Instance.Vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            _playerController.ChangeState(new WalkState(_playerController));
            
        }
    }

    public void Exit()
    {
        
    }
}
