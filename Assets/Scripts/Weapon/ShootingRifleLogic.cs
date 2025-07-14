using UnityEngine;
using UnityEngine.UI;

public class ShootingRifleLogic : MonoBehaviour
{

    [SerializeField]
    Image crosshairIcon;

    bool isShooting;

    public bool CanShootingRifle, CanShootPistol;

    private void OnEnable()
    {
        EventManager.StartShootEvent += BoolShootTrue;
        EventManager.StopShootEvent += BoolShootFalse;
    }


    private void OnDisable()
    {
        EventManager.StartShootEvent -= BoolShootTrue;
        EventManager.StopShootEvent -= BoolShootFalse;
    }

    private void Update()
    {
        if(isShooting && CanShootingRifle) TryShooting();
    }

    public void TryShooting()

    {
        Ray ray = Camera.main.ScreenPointToRay(new Vector2(Screen.width / 2, Screen.height / 2));
        RaycastHit hit;
    
        if(Physics.Raycast(ray, out hit, 1000))
        {
            Debug.Log("Вы попали в :" + hit.collider.name);

            if (hit.collider.tag == "NPC")
            {
                Debug.Log("NPC !!");
                if (crosshairIcon) crosshairIcon.color = Color.red;
                NPCHealth npcHealth =  hit.collider.gameObject.GetComponent<NPCHealth>();
                if(npcHealth) npcHealth.TakeDamage(10);

            }

            else
            {
                if(crosshairIcon) crosshairIcon.color = Color.black;
            }

        }

    }

    void BoolShootTrue() => isShooting = true;
    void BoolShootFalse() => isShooting = false;



}
