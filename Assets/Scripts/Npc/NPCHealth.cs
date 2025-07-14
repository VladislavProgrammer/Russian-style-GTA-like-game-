using UnityEngine;

public class NPCHealth : MonoBehaviour
{

    [SerializeField]
    private int _healthPoints = 100;

    [SerializeField]
    private NPCAnimatorManager npcAnimatorManager;

    [SerializeField]
    private RagdollLogic ragdollLogic;

    [SerializeField]
    private ParticleSystem[] bulletImpacts;

    [SerializeField]
    private NPCMovement npcMovement;
    
    public void TakeDamage(int damage)
    {
        if (_healthPoints > 0)
        {
            _healthPoints -= damage;
            PlayRandomEffect();
        }

        else
        {
            Die();
        }

    }


        
    public void Die()
    {
        Debug.Log("npc �����");
        if (ragdollLogic)
        {
            ragdollLogic.OnRagDoll();
        }
        else npcAnimatorManager.SetDeathAnim();
        gameObject.GetComponent<NPCHealth>().enabled = false;
        gameObject.GetComponent<BoxCollider>().enabled = false;
        npcMovement.CanMove = false;
        npcMovement.enabled = false;
        Invoke("Deactive", 10);
    }
    void Deactive() => gameObject.SetActive(false);

    void PlayRandomEffect()
    {
        int randomIndex = Random.Range(0, bulletImpacts.Length - 1);
        bulletImpacts[randomIndex].Play();
    }
        
}
