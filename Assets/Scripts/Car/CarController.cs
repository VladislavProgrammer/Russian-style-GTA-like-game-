using UnityEngine;

public class CarController : MonoBehaviour, IMovable
{

    [SerializeField]
    private WheelCollider leftFront, rightFront;

    [SerializeField]
    private WheelCollider leftBack, rightBack;

    [SerializeField]
    private Transform lFront, rFront;

    [SerializeField]
    private Transform lBack, rBack;

    [SerializeField]
    private float _whellForce;

    [SerializeField]
    private float _rotateAngle, _rotateForce;

    private float rpmLeft, rpmRight;
    
    public float rpmEngine;

    public float motorTorque;

    public bool CanMove;

    void Update()
    {
        RotateWheels();
    }

    private void FixedUpdate()
    {
        if (CanMove) TryMove();
        Engine();
    }


    public void TryMove()
    {
        leftBack.motorTorque = InputManager.Instance.Vertical * motorTorque * 5 * _whellForce;
        rightBack.motorTorque = InputManager.Instance.Vertical * motorTorque  * 5* _whellForce;

        rightFront.steerAngle = InputManager.Instance.Horizontal * _rotateAngle;
        leftFront.steerAngle = InputManager.Instance.Horizontal * _rotateAngle;
    }

    void RotateWheels()
    {
        Vector3 FRposi;
        Quaternion FRquator;
        rightFront.GetWorldPose(out FRposi, out FRquator);
        rFront.position = FRposi;
        rFront.rotation = FRquator;

        Vector3 FLposi;
        Quaternion FLquator;
        leftFront.GetWorldPose(out FLposi, out FLquator);
        lFront.position = FLposi;
        lFront.rotation = FLquator;

        Vector3 BRposi;
        Quaternion BRquator;
        rightBack.GetWorldPose(out BRposi, out BRquator);
        rBack.position = BRposi;
        rBack.rotation = BRquator;

        Vector3 BLposi;
        Quaternion BLquator;
        leftBack.GetWorldPose(out BLposi, out BLquator);
        lBack.position = BLposi;
        lBack.rotation = BLquator;
    }

    void Engine()
    {

        if (rpmRight >= rpmLeft)
        {
            rpmEngine = rpmRight;
        }

        else
        {
            rpmEngine = rpmLeft;
        }

        rpmRight = rightFront.rpm;
        rpmLeft = leftFront.rpm;

        if (rpmEngine < 2700f)
        {
            motorTorque = 100;
        }

        if (rpmEngine > 2700f && rpmEngine < 3200f)
        {
            motorTorque = 0.29f * rpmEngine - 683;
        }

        if (rpmEngine > 3700f && rpmEngine < 4200f)
        {
            motorTorque = (-0.01f) * rpmEngine + 372;
        }

        if (rpmEngine > 4200f && rpmEngine < 4700f)
        {
            motorTorque = 0.01f * rpmEngine + 243;
        }

        if (rpmEngine > 4700f && rpmEngine < 5200f)
        {
            motorTorque = -0.02f * rpmEngine + 384;
        }

        if (rpmEngine > 5200f && rpmEngine < 5700f)
        {
            motorTorque = -0.1f * rpmEngine + 800;
        }

        if (rpmEngine > 5700f && rpmEngine < 6200f)
        {
            motorTorque = -0.1f * rpmEngine + 800;
        }

        if (rpmEngine > 6200f && rpmEngine < 9000f)
        {
            motorTorque = -0.06f * rpmEngine + 552;
        }
    }
}



