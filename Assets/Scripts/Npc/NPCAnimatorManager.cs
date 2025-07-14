using UnityEngine;

public class NPCAnimatorManager : MonoBehaviour
{
    private Animator _animator;

    public AnimStates AnimState;


    [System.Serializable]
    public enum AnimStates
    {
        idle,
        walk, 
        talk,
        run
    }


    private void OnEnable()
    {
        _animator = GetComponent<Animator>();
        SetAnimState();
    }

   public void SetAnimState()
    {
        Debug.Log("Состояние анимации:" + AnimState);
        switch (AnimState)
        {
            case AnimStates.idle: // idle
                _animator.SetTrigger("idle");
                break;
            case AnimStates.walk: // walk
                _animator.SetTrigger("walk");
                break;
            case AnimStates.talk:
                _animator.SetBool("talk", true);
                break;
            case AnimStates.run:
                _animator.SetTrigger("run");
                break;
        }
    }


    public void SetDeathAnim() => _animator.SetTrigger("death");

    public void SetRunAnim() => _animator.SetBool("run", true);

    public void SetDialogAnim() => _animator.SetTrigger("talk");

	public void SetWalkAnim() => _animator.SetTrigger("walk");
    

    public void StopRunAnim() => _animator.SetBool("run", false);



    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player") SetAnimState();

    }
}
