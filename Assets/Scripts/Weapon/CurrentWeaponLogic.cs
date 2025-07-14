using UnityEngine;



public class CurrentWeaponLogic : MonoBehaviour
{
    [System.Serializable]
    public enum Weapon
    {
        MP5,
        AK47,
        Pistol
    }

    public Weapon currentWeapon;

    [SerializeField]
    ShootingRifleLogic shootingRifleLogic;

    [SerializeField]
    StateChanger stateChanger;

    [SerializeField]
    private GameObject[] WeaponsObj;

    [SerializeField]
    private GameObject[] Icons;

    private void Awake()
    {
        DeactiveAllWeaponsObj();
        HideAllIcons();
    }

    private void OnEnable()
    {
        EventManager.StartShootEvent += ShowCurrentWeapon;
        EventManager.StopShootEvent += DeactiveAllWeaponsObj;
    }

    private void OnDisable()
    {
        EventManager.StartShootEvent -= ShowCurrentWeapon;
        EventManager.StopShootEvent -= DeactiveAllWeaponsObj;
    }


    public void SetCurrentWeapon(int id)
    {
        if (id == 0) SetMP5();
        if (id == 1) SetAK47();
        if (id == 2) SetPistol();
    }

    void ShowCurrentWeapon()
    {
        DeactiveAllWeaponsObj();
        if (currentWeapon == Weapon.MP5) WeaponsObj[0].SetActive(true);
        if(currentWeapon == Weapon.AK47) WeaponsObj[1].SetActive(true);
        if (currentWeapon == Weapon.Pistol) WeaponsObj[2].SetActive(true);

    }


    void HideAllIcons()
    {
        foreach(var icon in Icons)
        {
            icon.SetActive(false);
        }
    }
    public void SetMP5()
    {
        currentWeapon = Weapon.MP5;
        stateChanger.CanShoot = true;
        shootingRifleLogic.CanShootingRifle = true;
        HideAllIcons();
        Icons[0].SetActive(true);

    }

    public void SetAK47()
    {
        currentWeapon = Weapon.AK47;
        stateChanger.CanShoot = true;
        shootingRifleLogic.CanShootingRifle = true;
        HideAllIcons();
        Icons[1].SetActive(true);
    }

    public void SetPistol()
    {
        currentWeapon = Weapon.Pistol;
        stateChanger.CanShoot = true;
        HideAllIcons();
        Icons[2].SetActive(true);
    }


    void DeactiveAllWeaponsObj()
    {
        foreach (var weapon in WeaponsObj)
        {
            weapon.SetActive(false);
        }
    }

}
