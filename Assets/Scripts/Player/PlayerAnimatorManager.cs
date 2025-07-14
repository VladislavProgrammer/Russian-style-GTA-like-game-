using System;
using UnityEngine;

public class PlayerAnimatorManager : MonoBehaviour
{
    public static PlayerAnimatorManager Instance;
    [SerializeField] private Animator _animator;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    public void SetIdleAnim()
    {
        
    }

    public void SetWalkAnim() => _animator.SetBool("walk", true);
    public void OffWalkAnim() => _animator.SetBool("walk", false);
    public void SetRunAnim() => _animator.SetBool("run", true);
    public void OffRunAnim() => _animator.SetBool("run", false);
    public void SetJumpAnim() => _animator.SetBool("jump", true);
    public void OffJumpAnim() => _animator.SetBool("jump", false);
    public void OnShootAnim() => _animator.SetBool("shoot", true);
    public void OffShootAnim() => _animator.SetBool("shoot", false);

}
