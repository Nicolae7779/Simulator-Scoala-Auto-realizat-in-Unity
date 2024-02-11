using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LightCodeVaz2105 : MonoBehaviour
{
    public GameObject GarageLight;
    public GameObject Light;

    public Text Gears;


    public GameObject ShortPhase;
    public GameObject LongPhase;
    public GameObject LightGearR;

    [Space(25)]
    [Header("Brake")]
    public Renderer Center;
    public Renderer Down;

    [Header("Material Brake")]
    public Material BrakeLightON;
    public Material BrakeLightOFF;


    [Space(25)]
    [Header("The Headlights of the car are turned on")]
    public Renderer Middle1;
    public Renderer Middle2;
    public Renderer Middle3;
    public Renderer Middle4;

    [Header("Material Light ON")]
    public Material LightTurnON_ON;
    public Material LightTurnON_OFF;


    [Space(25)]
    public Renderer ReverseLight;
    public Renderer ReverseLight2;

    [Header("Material Light ON")]
    public Material ReverseLight_ON;
    public Material ReverseLight_OFF;


    [Header("Long Phase")]
    public Renderer Element1;
    public Renderer Element2;
    public Renderer Element3;
    public Renderer Element4;
    public Renderer Element5;


    [Header("Short Phase")]
    public Renderer Element6;
    public Renderer Element7;


    [Space(10)]
    [Header("Material Short/Long Phase")]
    public Material LightPhase_ON;
    public Material Element1_OFF;
    public Material Element2_OFF;
    public Material Element3_OFF;
    public Material Element4_OFF;
    public Material Element5_OFF;
    public Material Element6_OFF;
    public Material Element7_OFF;

    [Space(25)]
    public int CurrentGear;

    public bool HShift = true;

    public float BrakeInput;

    public bool istrue = false;





    public int gear;
    public string NeutralAndReverseGear;


    private void FixedUpdate()
    {
        if (GarageLight.activeSelf)
        {
            Light.SetActive(true);
        }
        else
        {
            Light.SetActive(false);
        }



        HandleMotor();


        if (int.TryParse(Gears.text, out gear))
        {
            Debug.Log(gear);
        }
        else
        {
            NeutralAndReverseGear = Gears.text;
            Debug.Log(NeutralAndReverseGear);
        }
    }

    private void HandleMotor()
    {
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            LogitechGSDK.DIJOYSTATE2ENGINES rec;
            rec = LogitechGSDK.LogiGetStateUnity(0);

            LogitechGSDK.DIJOYSTATE2ENGINES rec2;
            rec2 = LogitechGSDK.LogiGetStateUnity(0);
            HShifter(rec);
            butoane_volan(rec2);

            int brake = 32767 - rec.lRz;

            BrakeInput = brake / 65535f;

            // if (LightON.activeSelf)
            // {
            if (BrakeInput > 0.05f)
            {
                Center.material = BrakeLightON;
                Down.material = BrakeLightON;
            }
            else
            {
                Center.material = BrakeLightOFF;
                Down.material = BrakeLightOFF;
            }
        }
    }



    void HShifter(LogitechGSDK.DIJOYSTATE2ENGINES shifter)
    {
        if (NeutralAndReverseGear == "R") // R
        {
            ReverseLight.material = ReverseLight_ON;
            ReverseLight2.material = ReverseLight_ON;
            LightGearR.SetActive(true);
        }
        else if (NeutralAndReverseGear == "N")
        {
            ReverseLight.material = ReverseLight_OFF;
            ReverseLight2.material = ReverseLight_OFF;
            LightGearR.SetActive(false);
        }
    }




    public int count = 0;
    public bool buttonPressed = false;

    void butoane_volan(LogitechGSDK.DIJOYSTATE2ENGINES v_butoane)
    {
        bool buttonPressed1 = false;
 
        for (int i = 0; i < 128; i++)
        {
            if (v_butoane.rgbButtons[i] == 128)
            {
                if (i == 11)
                {
                    buttonPressed1 = true;
                }


                if (i == 11 && count == 0 && !buttonPressed)
                {
                    ShortPhase.SetActive(true);
                    LongPhase.SetActive(false);


                    Element6.material = LightPhase_ON;
                    Element7.material = LightPhase_ON;

                    Middle1.material = LightTurnON_ON;
                    Middle2.material = LightTurnON_ON;
                    Middle3.material = LightTurnON_ON;
                    Middle4.material = LightTurnON_ON;




                    if (count != 2)
                        count++;
                    else
                        count = 0;
                }

                else if (i == 11 && count == 1 && !buttonPressed)
                {
                    ShortPhase.SetActive(false);
                    LongPhase.SetActive(true);


                    Element1.material = LightPhase_ON;
                    Element2.material = LightPhase_ON;
                    Element3.material = LightPhase_ON;
                    Element4.material = LightPhase_ON;
                    Element5.material = LightPhase_ON;

                    Middle1.material = LightTurnON_ON;
                    Middle2.material = LightTurnON_ON;
                    Middle3.material = LightTurnON_ON;
                    Middle4.material = LightTurnON_ON;



                    if (count != 2)
                        count++;
                    else
                        count = 0;
                }

                else if (i == 11 && count == 2 && !buttonPressed)
                {
                    ShortPhase.SetActive(false);
                    LongPhase.SetActive(false);


                    Element1.material = Element1_OFF;
                    Element2.material = Element2_OFF;
                    Element3.material = Element3_OFF;
                    Element4.material = Element4_OFF;
                    Element5.material = Element5_OFF;
                    Element6.material = Element6_OFF;
                    Element7.material = Element7_OFF;

                    Middle1.material = LightTurnON_OFF;
                    Middle2.material = LightTurnON_OFF;
                    Middle3.material = LightTurnON_OFF;
                    Middle4.material = LightTurnON_OFF;




                    if (count != 2)
                        count++;
                    else
                        count = 0;
                }
            }
        }

        if (buttonPressed1 == false)
            buttonPressed = false;
        else
            buttonPressed = true;
    }
}