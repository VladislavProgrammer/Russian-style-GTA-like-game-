using UnityEngine;

public class CarTrigger : MonoBehaviour
{
    [SerializeField] 
    private ViewManager _viewManager;

    [SerializeField]
    StateChanger stateChanger;

    [SerializeField]
    Transform carTargetPoint;

    [SerializeField]
    CurrentCarLogic currentCarLogic;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _viewManager.ShowButtonStartDrive();
            SetThisComponents();
            stateChanger.CanDrive = true;
        }

        
        
    }


    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            _viewManager.HideButtonStartDrive();
            stateChanger.CanDrive = false;
        }
    }

    void SetThisComponents()
    {
        currentCarLogic.currentCarController = gameObject.GetComponent<RCC_CarControllerV4>();
        currentCarLogic.currentCarPoint = carTargetPoint;
    }

    

}
