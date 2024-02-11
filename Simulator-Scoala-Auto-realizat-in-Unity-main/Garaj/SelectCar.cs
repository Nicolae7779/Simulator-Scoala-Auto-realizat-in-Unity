using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;

public class SelectCar : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData properties;

    [Header("Canvas Cars")]
    public GameObject ArrowsCars;
    public GameObject CarsTEXT;
    public GameObject Speedometer;




    public GameObject ChangeCarsButtons;
    public GameObject AfterSelectingTheCarButtons;

    public GameObject Damper;

    public GameObject LightSalon;
    public GameObject PointLightOutside;

    [Header("Camera")]
    public GameObject CameraFata;
    public GameObject CameraSpate;

    [Header("After the car has been selected")]
    public GameObject AfterSelectCar;

    [Header("Select Car")]
    public GameObject ChangeAllCars;




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
                if (i == 0 && !buttonPressed)
                {
                    CameraFata.SetActive(false);
                    CameraSpate.SetActive(true);


                    ChangeAllCars.SetActive(false);


                    AfterSelectCar.SetActive(true);

                    Damper.SetActive(true);

                    LightSalon.SetActive(true);

                    PointLightOutside.SetActive(false);

                    ArrowsCars.SetActive(false);
                    CarsTEXT.SetActive(false);
                    Speedometer.SetActive(true);


                    ChangeCarsButtons.SetActive(false);
                    AfterSelectingTheCarButtons.SetActive(true);



                }
                buttonPressed1 = true;
            }
        }
        if (buttonPressed1 == false)
            buttonPressed = false;
        else
            buttonPressed = true;
    }
}
