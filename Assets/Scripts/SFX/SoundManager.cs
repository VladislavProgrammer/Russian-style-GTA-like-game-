using UnityEngine;

public class SoundManager : MonoBehaviour
{
    
    [SerializeField]
    private GameObject mp5ShootSound, ak47ShootSound, engineSound;

    [SerializeField]
    private AudioSource startDriveSound, closeCarDoorSound;

    [SerializeField]
    private AudioSource walkSound, runSound;

    [SerializeField]
    CurrentWeaponLogic currentWeaponLogic;

    private void OnEnable()
    {
        EventManager.StartDriveEvent += PlayStartDriveSound;
        EventManager.StopDriveEvent += StopEngineSound;
    }

    private void OnDisable()
    {
        EventManager.StartDriveEvent -= PlayStartDriveSound;
        EventManager.StopDriveEvent -= StopEngineSound;
    }
    public void PlayShootSound()
    {
        if (currentWeaponLogic.currentWeapon == CurrentWeaponLogic.Weapon.MP5)
        {
            mp5ShootSound.SetActive(true);
        }

        if (currentWeaponLogic.currentWeapon == CurrentWeaponLogic.Weapon.AK47)
        {
            ak47ShootSound.SetActive(true);
        }
    }
    public void StopShootSound()
    {
        if (currentWeaponLogic.currentWeapon == CurrentWeaponLogic.Weapon.MP5)
        {
            mp5ShootSound.SetActive(false);
        }

        if (currentWeaponLogic.currentWeapon == CurrentWeaponLogic.Weapon.AK47)
        {
            ak47ShootSound.SetActive(false);
        }
    }
    public void PlayStartDriveSound()
    {
        startDriveSound.Play();
        engineSound.SetActive(true);
    }
    void StopEngineSound()
    {
        engineSound.SetActive(false);
        startDriveSound.Stop();
        closeCarDoorSound.Play();
    }

    public void PlayWalkSound() => walkSound.gameObject.SetActive(true);
    public void StopWalkSound() => walkSound.gameObject.SetActive(false);
    public void PlayRunSound() => runSound.gameObject.SetActive(true);
    public void StopRunSound() => runSound.gameObject.SetActive(false);
    
}
