using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change4CamerasInsideCar : MonoBehaviour
{
    [Header("Camera")]
    public GameObject CameraInterior;
    public GameObject CameraMirrorLeft;
    public GameObject CameraMirrorRight;
    public GameObject CameraMirrorUp;
    public GameObject CameraMirrorDown;



    LogitechGSDK.LogiControllerPropertiesData properties;


    private void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
    }



    private void Update()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            switch (rec.rgdwPOV[0])
            {
                case (0):
                    CameraInterior.SetActive(false);
                    CameraMirrorUp.SetActive(true);
                    break;

                case (9000):
                    CameraInterior.SetActive(false);
                    CameraMirrorRight.SetActive(true);
                    break;

                case (18000):
                    CameraInterior.SetActive(false);
                    CameraMirrorDown.SetActive(true);
                    break;

                case (27000):
                    CameraInterior.SetActive(false);
                    CameraMirrorLeft.SetActive(true);
                    break;

                default:
                    CameraInterior.SetActive(true);
                    CameraMirrorUp.SetActive(false);
                    CameraMirrorRight.SetActive(false);
                    CameraMirrorDown.SetActive(false);
                    CameraMirrorLeft.SetActive(false);
                    break;
            }
        }
    }
}

















