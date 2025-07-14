using UnityEngine;

public class CurrentCarLogic : MonoBehaviour
{
    [HideInInspector] public RCC_CarControllerV4 currentCarController;
    
    [HideInInspector] public Transform currentCarPoint;

    [SerializeField]
    private CameraController cameraController;

    private void OnEnable()
    {
        EventManager.StartDriveEvent += ActiveCar;
        EventManager.StartDriveEvent += SetCurrentCarPoint;
        EventManager.StopDriveEvent += DeactiveCar;
        
    }


    private void OnDisable()
    {
        EventManager.StartDriveEvent -= ActiveCar;
        EventManager.StartDriveEvent -= SetCurrentCarPoint;
        EventManager.StopDriveEvent -= DeactiveCar;
        

    }

    void SetCurrentCarPoint() => cameraController.currentTarget = currentCarPoint;
    
    void ActiveCar() => currentCarController.ActiveEngine();
    
    void DeactiveCar() => currentCarController.DeactiveEngine();
}
