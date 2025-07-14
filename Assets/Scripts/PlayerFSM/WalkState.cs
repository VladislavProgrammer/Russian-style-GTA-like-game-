using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkState : IPlayerState
{
    private readonly PlayerController _playerController;

    public WalkState(PlayerController playerController)
    {
        _playerController = playerController;
    }
    
    public void Enter()
    {
        Debug.Log("Начали ходьбу");
        PlayerAnimatorManager.Instance.SetWalkAnim();
    }

    public void Update()
    {
        Vector3 direction = new Vector3(InputManager.Instance.Horizontal, 0f, InputManager.Instance.Vertical).normalized;

        if (direction.magnitude < 0.1f)
        {
            _playerController.ChangeState(new IdleState(_playerController));
        }
    }


    public void Exit()
    {
        
    }
}

