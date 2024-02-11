using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class RotireRoti : MonoBehaviour
{

    LogitechGSDK.LogiControllerPropertiesData proprieties;

    public float maxSteeringAngle;
    private float steerAngle;
    public float xAxes;


    public WheelCollider frontLeftWheelCollider;
    public WheelCollider frontRightWheelCollider;

    public Transform frontLeftWheelTransform;
    public Transform frontRightWheelTransform;

    void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
    }

    // Update is called once per frame
 
    private void FixedUpdate()
    {

        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
         {
             LogitechGSDK.DIJOYSTATE2ENGINES rec;
             rec = LogitechGSDK.LogiGetStateUnity(0);

            xAxes = rec.lX / 32768f;
            steerAngle = maxSteeringAngle * xAxes;
        }

         frontLeftWheelCollider.steerAngle = steerAngle;
         frontRightWheelCollider.steerAngle = steerAngle;


        UpdateWheelPos(frontLeftWheelCollider, frontLeftWheelTransform);
        UpdateWheelPos(frontRightWheelCollider, frontRightWheelTransform);


        void UpdateWheelPos(WheelCollider wheelCollider, Transform trans)
         {
              Vector3 pos;
              Quaternion rot;
              wheelCollider.GetWorldPose(out pos, out rot);
              trans.rotation = rot;
              trans.position = pos;
         }
    }
}
