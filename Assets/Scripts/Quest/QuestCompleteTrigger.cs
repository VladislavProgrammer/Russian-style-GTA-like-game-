using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestCompleteTrigger : MonoBehaviour
{
   private void OnTriggerEnter(Collider other)
   {
        if(other.gameObject.tag == "Player")
        {
            EventManager.QuestCompleteEvent();
            gameObject.SetActive(false);  
        }
   }
}
