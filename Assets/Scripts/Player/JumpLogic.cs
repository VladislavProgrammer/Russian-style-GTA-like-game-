using UnityEngine;

public class JumpLogic : MonoBehaviour
{
    [SerializeField]
    PlayerAnimatorManager _playerAnimatorManager;

    [SerializeField]
    private float _jumpForce = 2f;

    private float _gravity = -9.8f;

    private CharacterController cc;

    private Vector3 _velocity;


    private void Awake()
    {
        cc = GetComponent<CharacterController>();
    }


    void Update()
    {
        Jump();
    }

    private void Jump()
    {

        if (_velocity.y < 0)
        {
            _velocity.y = 0f;
        }

        if (InputManager.Instance.PushJumpButton && cc.isGrounded)
        {
            _velocity.y += Mathf.Sqrt(_jumpForce * -2f * _gravity);
            _playerAnimatorManager.SetJumpAnim();
            Debug.Log("ïðûæîê");
        }

        else _playerAnimatorManager.OffJumpAnim();

        _velocity.y += _gravity * Time.deltaTime * 5f;
        cc.Move(_velocity * Time.deltaTime);
    }
}
