using UnityEngine;

public class CameraPerspective : MonoBehaviour
{
    [SerializeField]
    private Transform shootingPoint, defaultPoint, drivingPoint;

    private void OnEnable()
    {
        EventManager.StartShootEvent += ShootingPerspective;
        EventManager.StopShootEvent += DefaultPerspective;

    }

    private void OnDisable()
    {
        EventManager.StartShootEvent -= ShootingPerspective;
        EventManager.StopShootEvent -= DefaultPerspective;


    }
    void ShootingPerspective() => Camera.main.transform.position = shootingPoint.position;
     void DefaultPerspective() => Camera.main.transform.position = defaultPoint.position;
     void DrivingPerspective() => Camera.main.transform.position = drivingPoint.position;

}
