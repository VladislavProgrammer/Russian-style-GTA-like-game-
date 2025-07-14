using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private IPlayerState _currentPlayerState;

    private void Awake()
    {
        ChangeState(new IdleState(this));
    }

    public void ChangeState(IPlayerState newPlayerState)
    {
        _currentPlayerState.Exit();
        _currentPlayerState = newPlayerState;
        _currentPlayerState.Enter();
    }
    
    
}
