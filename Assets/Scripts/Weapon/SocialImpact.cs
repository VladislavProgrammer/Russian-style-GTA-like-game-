using UnityEngine;

public class SocialImpact : MonoBehaviour
{
    public
    float Radius;

    private void OnEnable()
    {
        EventManager.StartShootEvent += SetFearSphere;
    }


    private void OnDisable()
    {
        EventManager.StartShootEvent -= SetFearSphere;

    }


    void SetFearSphere()
    {
        Collider[] hitColliders = Physics.OverlapSphere(transform.position, Radius);
        foreach(var hitCollider in hitColliders)
        {
            if(hitCollider.gameObject.tag == "NPC")
            {
                NPCMovement npcMovement = hitCollider.gameObject.GetComponent<NPCMovement>();
                if(npcMovement)
                {
                    Debug.Log("Напугали npc !!!!");
					npcMovement.isFear = true;
                    npcMovement.FearMovement();
                }
            }
        }
    }
}
