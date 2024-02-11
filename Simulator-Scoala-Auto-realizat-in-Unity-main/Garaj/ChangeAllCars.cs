using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChangeAllCars: MonoBehaviour
{

    [Header("Canvas, Camera Fata")]
    public Canvas canvas;
    public GameObject Camera_Fata;


    [Header("BMW Alpina")]
    public GameObject BMW_Alpina;
    public GameObject Text_BMW_Alpina;
    public GameObject Camera_Spate_BMW_Alpina;
    public GameObject Camera_Interior_BMW_Alpina;


    [Header("BMW i4")]
    public GameObject BMW_i4;
    public GameObject Text_BMW_i4;
    public GameObject Camera_Spate_BMW_i4;
    public GameObject Camera_Interior_BMW_i4;

    [Header("BMW M3")]
    public GameObject BMW_M3;
    public GameObject Text_BMW_M3;
    public GameObject Camera_Spate_BMW_M3;
    public GameObject Camera_Interior_BMW_M3;

    [Header("BMW M4")]
    public GameObject BMW_M4;
    public GameObject Text_BMW_M4;
    public GameObject Camera_Spate_BMW_M4;
    public GameObject Camera_Interior_BMW_M4;

    [Header("BMW X3")]
    public GameObject BMW_X3;
    public GameObject Text_BMW_X3;
    public GameObject Camera_Spate_BMW_X3;
    public GameObject Camera_Interior_BMW_X3;

    [Header("Ferrari F12")]
    public GameObject Ferrari_F12;
    public GameObject Text_Ferrari_F12;
    public GameObject Camera_Spate_Ferrari_F12;
    public GameObject Camera_Interior_Ferrari_F12;

    [Header("Lamborghini Huracan")]
    public GameObject Lamborghini_Huracan;
    public GameObject Text_Lamborghini_Huracan;
    public GameObject Camera_Spate_Lamborghini_Huracan;
    public GameObject Camera_Interior_Lamborghini_Huracan;

    [Header("Mercedes AMG GT R Roadster")]
    public GameObject Mercedes_AMG_GT_R_Roadster;
    public GameObject Text_Mercedes_AMG_GT_R_Roadster;
    public GameObject Camera_Spate_Mercedes_AMG_GT_R_Roadster;
    public GameObject Camera_Interior_Mercedes_AMG_GT_R_Roadster;

    [Header("Mercedes S Class Long")]
    public GameObject Mercedes_S_Class_Long;
    public GameObject Text_Mercedes_S_Class_Long;
    public GameObject Camera_Spate_Mercedes_S_Class_Long;
    public GameObject Camera_Interior_Mercedes_S_Class_Long;

    [Header("Porsche Taycan")]
    public GameObject Porsche_Taycan;
    public GameObject Text_Porsche_Taycan;
    public GameObject Camera_Spate_Porsche_Taycan;
    public GameObject Camera_Interior_Porsche_Taycan;

    [Header("Porsche Taycan Turbo S")]
    public GameObject Porsche_Taycan_Turbo_S;
    public GameObject Text_Porsche_Taycan_Turbo_S;
    public GameObject Camera_Spate_Porsche_Taycan_Turbo_S;
    public GameObject Camera_Interior_Porsche_Taycan_Turbo_S;

    [Header("Porsche 911 Speedester")]
    public GameObject Porshe_911_Speedester;
    public GameObject Text_Porshe_911_Speedester;
    public GameObject Camera_Spate_Porshe_911_Speedester;
    public GameObject Camera_Interior_Porshe_911_Speedester;

    [Header("Porsche 911 Turbo")]
    public GameObject Porshe_911_Turbo;
    public GameObject Text_Porshe_911_Turbo;
    public GameObject Camera_Spate_Porshe_911_Turbo;
    public GameObject Camera_Interior_Porshe_911_Turbo;

    [Header("Volkswagen Golf R Estate")]
    public GameObject Volkswagen_Golf_R_Estate;
    public GameObject Text_Volkswagen_Golf_R_Estate;
    public GameObject Camera_Spate_Volkswagen_Golf_R_Estate;
    public GameObject Camera_Interior_Volkswagen_Golf_R_Estate;

    [Header("Vaz_2105")]
    public GameObject Vaz_2105;
    public GameObject Text_Vaz_2105;
    public GameObject Camera_Spate_Vaz_2105;
    public GameObject Camera_Interior_Vaz_2105;




    [Header("Numarul Masinii / Camerei")]

    public int  CurrentCar = 1;

    public int CurrentCamera = 1;

    bool CarSelected;

    bool CameraSelected;

    LogitechGSDK.LogiControllerPropertiesData proprieties;


    private void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));

        //Text_BMW_Alpina = canvas.GetComponentInChildren<Text>();
        //Text_BMW_i4 = canvas.GetComponentsInChildren<Text>()[1];
        //Text_BMW_M3 = canvas.GetComponentsInChildren<Text>()[2];
        //Text_BMW_M4 = canvas.GetComponentsInChildren<Text>()[3];
        //Text_BMW_X3 = canvas.GetComponentsInChildren<Text>()[4];
        //Text_Ferrari_F12 = canvas.GetComponentsInChildren<Text>()[5];
        //Text_Lamborghini_Huracan = canvas.GetComponentsInChildren<Text>()[6];
        //Text_Mercedes_AMG_GT_R_Roadster = canvas.GetComponentsInChildren<Text>()[7];
        //Text_Mercedes_S_Class_Long = canvas.GetComponentsInChildren<Text>()[8];
        //Text_Porsche_Taycan = canvas.GetComponentsInChildren<Text>()[9];
        //Text_Porsche_Taycan_Turbo_S = canvas.GetComponentsInChildren<Text>()[10];
        //Text_Porshe_911_Speedester = canvas.GetComponentsInChildren<Text>()[11];
        //Text_Porshe_911_Turbo = canvas.GetComponentsInChildren<Text>()[12];
        //Text_Volkswagen_Golf_R_Estate = canvas.GetComponentsInChildren<Text>()[13];
        //Text_Vaz_2105 = canvas.GetComponentsInChildren<Text>()[14];
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
                Debug.Log(i);
                if (i == 3 && !buttonPressed)   // triangle
                {
                    if (CurrentCamera < 4)
                        CurrentCamera++;
                    CameraSelected = true;
                }


                if (i == 2  && !buttonPressed)   // Circle
                {
                    if (CurrentCar < 16)
                        CurrentCar++;
                    CarSelected = true;
                }


                if (i == 1 && !buttonPressed)   // Square 
                {
                    if (CurrentCar > 0)
                        CurrentCar--;
                    CarSelected = true;
                }
                buttonPressed1 = true;
            }
        }
        if (buttonPressed1 == false)
            buttonPressed = false;
        else
            buttonPressed = true;


        if (CurrentCar == 0)
        {
            CurrentCar = 15;
        }
        else if (CurrentCar == 1)
        {
            BMW_Alpina.SetActive(true);
            BMW_i4.SetActive(false);
            Vaz_2105.SetActive(false);

            Text_BMW_Alpina.SetActive(true);
            Text_BMW_i4.SetActive(false);
            Text_Vaz_2105.SetActive(false);
        }
        else if (CurrentCar == 2)
        {
            BMW_Alpina.SetActive(false);
            BMW_i4.SetActive(true);
            BMW_M3.SetActive(false);

            Text_BMW_Alpina.SetActive(false);
            Text_BMW_i4.SetActive(true);
            Text_BMW_M3.SetActive(false);
        }
        else if (CurrentCar == 3)
        {
            BMW_i4.SetActive(false);
            BMW_M3.SetActive(true);
            BMW_M4.SetActive(false);

            Text_BMW_i4.SetActive(false);
            Text_BMW_M3.SetActive(true);
            Text_BMW_M4.SetActive(false);
        }
        else if (CurrentCar == 4)
        {
            BMW_M3.SetActive(false);
            BMW_M4.SetActive(true);
            BMW_X3.SetActive(false);

            Text_BMW_M3.SetActive(false);
            Text_BMW_M4.SetActive(true);
            Text_BMW_X3.SetActive(false);
        }
        else if (CurrentCar == 5)
        {
            BMW_M4.SetActive(false);
            BMW_X3.SetActive(true);
            Ferrari_F12.SetActive(false);

            Text_BMW_M4.SetActive(false);
            Text_BMW_X3.SetActive(true);
            Text_Ferrari_F12.SetActive(false);
        }
        else if (CurrentCar == 6)
        {
            BMW_X3.SetActive(false);
            Ferrari_F12.SetActive(true);
            Lamborghini_Huracan.SetActive(false);

            Text_BMW_X3.SetActive(false);
            Text_Ferrari_F12.SetActive(true);
            Text_Lamborghini_Huracan.SetActive(false);
        }
        else if (CurrentCar == 7)
        {
            Ferrari_F12.SetActive(false);
            Lamborghini_Huracan.SetActive(true);
            Mercedes_AMG_GT_R_Roadster.SetActive(false);

            Text_Ferrari_F12.SetActive(false);
            Text_Lamborghini_Huracan.SetActive(true);
            Text_Mercedes_AMG_GT_R_Roadster.SetActive(false);
        }
        else if (CurrentCar == 8)
        {
            Lamborghini_Huracan.SetActive(false);
            Mercedes_AMG_GT_R_Roadster.SetActive(true);
            Mercedes_S_Class_Long.SetActive(false);

            Text_Lamborghini_Huracan.SetActive(false);
            Text_Mercedes_AMG_GT_R_Roadster.SetActive(true);
            Text_Mercedes_S_Class_Long.SetActive(false);
        }
        else if (CurrentCar == 9)
        {
            Mercedes_AMG_GT_R_Roadster.SetActive(false);
            Mercedes_S_Class_Long.SetActive(true);
            Porsche_Taycan.SetActive(false);

            Text_Mercedes_AMG_GT_R_Roadster.SetActive(false);
            Text_Mercedes_S_Class_Long.SetActive(true);
            Text_Porsche_Taycan.SetActive(false);
        }
        else if (CurrentCar == 10)
        {
            Mercedes_S_Class_Long.SetActive(false);
            Porsche_Taycan.SetActive(true);
            Porsche_Taycan_Turbo_S.SetActive(false);

            Text_Mercedes_S_Class_Long.SetActive(false);
            Text_Porsche_Taycan.SetActive(true);
            Text_Porsche_Taycan_Turbo_S.SetActive(false);
        }
        else if (CurrentCar == 11)
        {
            Porsche_Taycan.SetActive(false);
            Porsche_Taycan_Turbo_S.SetActive(true);
            Porshe_911_Speedester.SetActive(false);

            Text_Porsche_Taycan.SetActive(false);
            Text_Porsche_Taycan_Turbo_S.SetActive(true);
            Text_Porshe_911_Speedester.SetActive(false);
        }
        else if (CurrentCar == 12)
        {
            Porsche_Taycan_Turbo_S.SetActive(false);
            Porshe_911_Speedester.SetActive(true);
            Porshe_911_Turbo.SetActive(false);

            Text_Porsche_Taycan_Turbo_S.SetActive(false);
            Text_Porshe_911_Speedester.SetActive(true);
            Text_Porshe_911_Turbo.SetActive(false);
        }
        else if (CurrentCar == 13)
        {
            Porshe_911_Speedester.SetActive(false);
            Porshe_911_Turbo.SetActive(true);
            Volkswagen_Golf_R_Estate.SetActive(false);

            Text_Porshe_911_Speedester.SetActive(false);
            Text_Porshe_911_Turbo.SetActive(true);
            Text_Volkswagen_Golf_R_Estate.SetActive(false);
        }
        else if (CurrentCar == 14)
        {
            Porshe_911_Turbo.SetActive(false);
            Volkswagen_Golf_R_Estate.SetActive(true);
            Vaz_2105.SetActive(false);

            Text_Porshe_911_Turbo.SetActive(false);
            Text_Volkswagen_Golf_R_Estate.SetActive(true);
            Text_Vaz_2105.SetActive(false);
        }
        else if (CurrentCar == 15)
        {
            Volkswagen_Golf_R_Estate.SetActive(false);
            Vaz_2105.SetActive(true);
            BMW_Alpina.SetActive(false);

            Text_Volkswagen_Golf_R_Estate.SetActive(false);
            Text_Vaz_2105.SetActive(true);
            Text_BMW_Alpina.SetActive(false);
        }
        else if (CurrentCar == 16)
        {
            CurrentCar = 1;
        }



        if (CurrentCamera == 1)
        {
            Camera_Fata.SetActive(true);

            Camera_Spate_BMW_Alpina.SetActive(false);
            Camera_Interior_BMW_Alpina.SetActive(false);

            Camera_Spate_BMW_i4.SetActive(false);
            Camera_Interior_BMW_i4.SetActive(false);

            Camera_Spate_BMW_M3.SetActive(false);
            Camera_Interior_BMW_M3.SetActive(false);

            Camera_Spate_BMW_M4.SetActive(false);
            Camera_Interior_BMW_M4.SetActive(false);

            Camera_Spate_BMW_X3.SetActive(false);
            Camera_Interior_BMW_X3.SetActive(false);

            Camera_Spate_Ferrari_F12.SetActive(false);
            Camera_Interior_Ferrari_F12.SetActive(false);

            Camera_Spate_Lamborghini_Huracan.SetActive(false);
            Camera_Interior_Lamborghini_Huracan.SetActive(false);

            Camera_Spate_Mercedes_AMG_GT_R_Roadster.SetActive(false);
            Camera_Interior_Mercedes_AMG_GT_R_Roadster.SetActive(false);

            Camera_Spate_Mercedes_S_Class_Long.SetActive(false);
            Camera_Interior_Mercedes_S_Class_Long.SetActive(false);

            Camera_Spate_Porsche_Taycan.SetActive(false);
            Camera_Interior_Porsche_Taycan.SetActive(false);

            Camera_Spate_Porsche_Taycan_Turbo_S.SetActive(false);
            Camera_Interior_Porsche_Taycan_Turbo_S.SetActive(false);

            Camera_Spate_Porshe_911_Speedester.SetActive(false);
            Camera_Interior_Porshe_911_Speedester.SetActive(false);

            Camera_Spate_Porshe_911_Turbo.SetActive(false);
            Camera_Interior_Porshe_911_Turbo.SetActive(false);

            Camera_Spate_Volkswagen_Golf_R_Estate.SetActive(false);
            Camera_Interior_Volkswagen_Golf_R_Estate.SetActive(false);

            Camera_Spate_Vaz_2105.SetActive(false);
            Camera_Interior_Vaz_2105.SetActive(false);

        }
        else if (CurrentCamera == 2 && CurrentCar == 1)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_Alpina.SetActive(true);
            Camera_Interior_BMW_Alpina.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 2)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_i4.SetActive(true);
            Camera_Interior_BMW_i4.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 3)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_M3.SetActive(true);
            Camera_Interior_BMW_M3.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 4)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_M4.SetActive(true);
            Camera_Interior_BMW_M4.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 5)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_X3.SetActive(true);
            Camera_Interior_BMW_X3.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 6)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Ferrari_F12.SetActive(true);
            Camera_Interior_Ferrari_F12.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 7)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Lamborghini_Huracan.SetActive(true);
            Camera_Interior_Lamborghini_Huracan.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 8)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Mercedes_AMG_GT_R_Roadster.SetActive(true);
            Camera_Interior_Mercedes_AMG_GT_R_Roadster.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 9)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Mercedes_S_Class_Long.SetActive(true);
            Camera_Interior_Mercedes_S_Class_Long.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 10)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Porsche_Taycan.SetActive(true);
            Camera_Interior_Porsche_Taycan.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 11)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Porsche_Taycan_Turbo_S.SetActive(true);
            Camera_Interior_Porsche_Taycan_Turbo_S.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 12)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Porshe_911_Speedester.SetActive(true);
            Camera_Interior_Porshe_911_Speedester.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 13)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Porshe_911_Turbo.SetActive(true);
            Camera_Interior_Porshe_911_Turbo.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 14)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Volkswagen_Golf_R_Estate.SetActive(true);
            Camera_Interior_Volkswagen_Golf_R_Estate.SetActive(false);
        }
        else if (CurrentCamera == 2 && CurrentCar == 15)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Vaz_2105.SetActive(true);
            Camera_Interior_Vaz_2105.SetActive(false);
        }
        else if (CurrentCamera == 3 && CurrentCar == 1)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_Alpina.SetActive(false);
            Camera_Interior_BMW_Alpina.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 2)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_i4.SetActive(false);
            Camera_Interior_BMW_i4.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 3)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_M3.SetActive(false);
            Camera_Interior_BMW_M3.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 4)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_M4.SetActive(false);
            Camera_Interior_BMW_M4.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 5)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_BMW_X3.SetActive(false);
            Camera_Interior_BMW_X3.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 6)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Ferrari_F12.SetActive(false);
            Camera_Interior_Ferrari_F12.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 7)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Lamborghini_Huracan.SetActive(false);
            Camera_Interior_Lamborghini_Huracan.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 8)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Mercedes_AMG_GT_R_Roadster.SetActive(false);
            Camera_Interior_Mercedes_AMG_GT_R_Roadster.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 9)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Mercedes_S_Class_Long.SetActive(false);
            Camera_Interior_Mercedes_S_Class_Long.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 10)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Porsche_Taycan.SetActive(false);
            Camera_Interior_Porsche_Taycan.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 11)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Porsche_Taycan_Turbo_S.SetActive(false);
            Camera_Interior_Porsche_Taycan_Turbo_S.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 12)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Porshe_911_Speedester.SetActive(false);
            Camera_Interior_Porshe_911_Speedester.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 13)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Porshe_911_Turbo.SetActive(false);
            Camera_Interior_Porshe_911_Turbo.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 14)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Volkswagen_Golf_R_Estate.SetActive(false);
            Camera_Interior_Volkswagen_Golf_R_Estate.SetActive(true);
        }
        else if (CurrentCamera == 3 && CurrentCar == 15)
        {
            Camera_Fata.SetActive(false);
            Camera_Spate_Vaz_2105.SetActive(false);
            Camera_Interior_Vaz_2105.SetActive(true);
        }
        else if (CurrentCamera == 4)
        {
            CurrentCamera = 1;
        }
    }
}
