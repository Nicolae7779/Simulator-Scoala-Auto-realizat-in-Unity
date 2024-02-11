using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class StartEngine : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData proprieties;

    private bool isButtonPressed = false;

    public float ButtonPressTime = 0f;

    public float StartEngineTime = 5.39f;



    public float CurrentRPM = 0f;

    public Text RpmText;

    public AudioSource sound;
    private bool isPlaying = false;
    private bool hasPlayed = false;



    public GameObject DeselectStartEngine;
    public GameObject StartEngineRPM;
    public GameObject Idle;



    public Text Gears;
    public int gear;
    public string NeutralAndReverseGear;

    public GameObject ErrorMessage;
    public Text ErrorMessageText;


    private void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
    }

    private void FixedUpdate()
    {
        if (int.TryParse(Gears.text, out gear))
        {
            Debug.Log(gear);
        }
        else
        {
            NeutralAndReverseGear = Gears.text;
            Debug.Log(NeutralAndReverseGear);
        }
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

    void butoane_volan(LogitechGSDK.DIJOYSTATE2ENGINES v_butoane)
    {




        for (int i = 0; i < 128; i++)
        {
            if (v_butoane.rgbButtons[i] == 128)
            {
                if (i == 23 && ((gear >= 1 && gear <= 6) || NeutralAndReverseGear == "R"))
                {
                    ErrorMessage.SetActive(true);
                    ErrorMessageText.text = "Trebuie să pornești motorul mașinii atunci când schimbătorul de viteză este în treapta Neutră";
                }
                else if (i == 23 && NeutralAndReverseGear == "N")
                {
                    ErrorMessageText.text = "";
                    ErrorMessage.SetActive(false);
                    isButtonPressed = true;
                }
                else
                {
                    sound.Stop();
                    isButtonPressed = false;
                }


                if (isButtonPressed)
                {
                    ButtonPressTime += Time.deltaTime;
                }

                if (ButtonPressTime <= StartEngineTime && !isPlaying && !hasPlayed)
                {
                    sound.Play();
                    isPlaying = true;
                    hasPlayed = true;           
                }

                if (ButtonPressTime >= StartEngineTime)                                            
                {
                    //Debug.Log("sa deselectat engine");
                    hasPlayed = false;
                    DeselectStartEngine.SetActive(false);
                    StartEngineRPM.SetActive(true);
                }


                if (ButtonPressTime <= StartEngineTime)
                {

                    CurrentRPM = Mathf.Lerp(0f, 110f, ButtonPressTime / 0.5f);
                    RpmText.text = CurrentRPM.ToString("F0");
                }


                if (DeselectStartEngine.activeSelf)
                {
                    Idle.SetActive(false);
                }
                else
                    Idle.SetActive(true);

            }
            else if (v_butoane.rgbButtons[i] != 128 && i == 23)
            {
                isButtonPressed = false;
                ButtonPressTime = 0f;
                isPlaying = false;
                sound.Stop();
                hasPlayed = false; //????????
                CurrentRPM = 0f;
                RpmText.text = CurrentRPM.ToString("F0");
            }
        }
    }
}
