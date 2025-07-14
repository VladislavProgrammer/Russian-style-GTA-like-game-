using UnityEngine;

public class WeaponTrigger : MonoBehaviour
{
    [SerializeField]
    private CurrentWeaponLogic currentWeaponLogic;

    [SerializeField]
    private float _rotateForce = 300f;

    [SerializeField]
    private StateChanger stateChanger;

    [SerializeField]
    public int WeaponID; // 0 - MP5,  1-AK47

    [SerializeField]
    private Transform rotationTransform;

    [SerializeField]
    private AudioSource pickUpWeaponSound;

    private bool canRotate = true;
    private void Update()
    {
        if(canRotate) RotateObj();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            PickUp();
        }
       
    }

    void PickUp()
    {
        currentWeaponLogic.SetCurrentWeapon(WeaponID);
        pickUpWeaponSound.Play();
        canRotate = false;
        stateChanger.PickedWeapon = true;
        gameObject.SetActive(false);
    }
    
    void RotateObj()
    {
      if(rotationTransform) rotationTransform.Rotate(0, _rotateForce * Time.deltaTime, 0);
    }

}
