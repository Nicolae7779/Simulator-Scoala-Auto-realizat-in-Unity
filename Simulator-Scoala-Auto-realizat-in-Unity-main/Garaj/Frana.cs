using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Frana : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData proprieties;

    public float BrakePower;
    public float BrakePedal;
    public float BrakeForce;

    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;
    public WheelCollider rearLeftWheelCollider;
    public WheelCollider rearRightWheelCollider;

    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;
    public Transform rearLeftWheelTransform;
    public Transform rearRightWheelTransform;

    void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
    }

    private void FixedUpdate()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            int brake = 32767 - rec.lRz;
            BrakePedal = brake / 655.35f;
            BrakeForce = BrakePedal * BrakePower;
        }

        frontLeftWheelCollider.brakeTorque = BrakeForce;
        frontRightWheelCollider.brakeTorque = BrakeForce;
        rearLeftWheelCollider.brakeTorque = BrakeForce;
        rearRightWheelCollider.brakeTorque = BrakeForce;

        //frontLeftWheelCollider.motorTorque = 0f;
        //frontRightWheelCollider.motorTorque = 0f;
        //rearLeftWheelCollider.motorTorque = 0f;
        //rearRightWheelCollider.motorTorque = 0f;

        //frontLeftWheelCollider.steerAngle = 0f;
        //frontRightWheelCollider.steerAngle = 0f;

        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);
    }

    void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);
        trans.rotation = rot;
        trans.position = pos;
    }
}


















//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class Frana : MonoBehaviour
//{
//    LogitechGSDK.LogiControllerPropertiesData proprieties;



//    public float BrakePower;

//    public float BrakePedal;

//    private float BrakeForce;

//    public WheelCollider frontLeftWheelCollider;
//    public WheelCollider frontRightWheelCollider;
//    public WheelCollider rearLeftWheelCollider;
//    public WheelCollider rearRightWheelCollider;

//    public Transform frontLeftWheelTransform;
//    public Transform frontRightWheelTransform;
//    public Transform rearLeftWheelTransform;
//    public Transform rearRightWheelTransform;














//    void Start()
//    {
//        print(LogitechGSDK.LogiSteeringInitialize(false));
//    }


//    private void FixedUpdate()
//    {
//        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
//        {
//            LogitechGSDK.DIJOYSTATE2ENGINES rec;
//            rec = LogitechGSDK.LogiGetStateUnity(0);

//            int brake = 32767 - rec.lRz;
//            //Debug.Log(rec.lRz);
//            BrakePedal = brake / 655.35f;

//            BrakeForce = BrakePedal * BrakePower;

//        }

//        frontLeftWheelCollider.brakeTorque = BrakeForce;
//        frontRightWheelCollider.brakeTorque = BrakeForce;
//        rearLeftWheelCollider.brakeTorque = BrakeForce;
//        rearRightWheelCollider.brakeTorque = BrakeForce;

//        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
//        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);
//        UpdateWheelPos(rearLeftWheelCollider, rearLeftWheelTransform);
//        UpdateWheelPos(rearRightWheelCollider, rearRightWheelTransform);


//        void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
//        {
//            Vector3 pos;
//            Quaternion rot;
//            wheelCollider.GetWorldPose(out pos, out rot);
//            trans.rotation = rot;
//            trans.position = pos;
//        }
//    }
//}


