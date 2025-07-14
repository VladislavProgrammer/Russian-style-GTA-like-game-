using UnityEngine;

public class NpcHitTrigger : MonoBehaviour
{
    
    [SerializeField]
    private AudioSource carCrashSound;
    
    [SerializeField]
    private CurrentCarLogic currentCarLogic;

    


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "NPC")
        {
            if (currentCarLogic.currentCarController != null)
            {
                if (currentCarLogic.currentCarController.speed > 10)
                {
                    NPCHealth npcHealth = other.gameObject.GetComponent<NPCHealth>();
                    if(npcHealth) npcHealth.Die();
                    carCrashSound.Play();
                }
                
            }
                
        }
    }
}
