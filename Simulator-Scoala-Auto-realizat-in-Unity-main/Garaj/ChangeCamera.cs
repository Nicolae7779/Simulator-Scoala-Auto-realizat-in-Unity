using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
    [Header("Camera")]
    public GameObject CameraSpate;
    public GameObject CameraInterior;
    public GameObject CameraInsideCar;

    public GameObject CameraInsideButtons;

    public int CurrentCamera = 2;


    LogitechGSDK.LogiControllerPropertiesData proprieties;

    private void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
    }


    private void FixedUpdate()
    {
        Butoane_Volan();
    }


    private void Butoane_Volan()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            butoane_volan(rec);
        }
    }

    public bool buttonPressed = false;

    void butoane_volan(LogitechGSDK.DIJOYSTATE2ENGINES v_butoane)
    {
        bool buttonPressed1 = false;
        for (int i = 0; i < 128; i++)
        {
            if (v_butoane.rgbButtons[i] == 128)
            {
                if (i == 3 && !buttonPressed)
                {
                    if (CurrentCamera < 4)
                        CurrentCamera++;
                }
                buttonPressed1 = true;

                if (CurrentCamera == 2)
                {              
                    CameraSpate.SetActive(true);
                    CameraInterior.SetActive(false);
                    CameraInsideCar.SetActive(false);

                    CameraInsideButtons.SetActive(false);

                }
                else if (CurrentCamera == 3)
                {
                    CameraSpate.SetActive(false);
                    CameraInterior.SetActive(true);
                    CameraInsideCar.SetActive(true);

                    CameraInsideButtons.SetActive(true);


                }      
                else if (CurrentCamera == 4)
                {
                    CurrentCamera = 2;
                }

            }
        }
        if (buttonPressed1 == false)
            buttonPressed = false;
        else
            buttonPressed = true;
    }
}
