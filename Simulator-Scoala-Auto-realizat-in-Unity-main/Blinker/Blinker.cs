using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class Blinker : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData properties;

    public bool buttonPressed = false;

    public int count = 0;

    public int SignalLeft;
    public int SignalRight;

    public AudioSource SignalStart;
    public AudioSource SignalLoop;
    public AudioSource SignalEnd;

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
            butoane_volan(rec);
        }



        void butoane_volan(LogitechGSDK.DIJOYSTATE2ENGINES v_butoane)
        {
            bool buttonPressed1 = false;
            for (int i = 0; i < 128; i++)
            {
                if (v_butoane.rgbButtons[i] == 128)
                {
                    if (i == 4 && !buttonPressed)
                    {
                        SignalStart.Play();
                        StartCoroutine(PlayDelayedSound());
                        count += 1;
                    }
                    else if (i == 5 && !buttonPressed)
                    {
                        SignalStart.Play();
                        StartCoroutine(PlayDelayedSound());
                        count += 1;
                    }
                    if (count == 2)
                    {
                        SignalStart.Stop();
                        SignalEnd.Play();
                    }
                    else if (count == 3)
                    {
                        count = 1;
                    }

              
                    buttonPressed1 = true;
                }
            }
            if (buttonPressed1 == false) 
            {
                buttonPressed = false;
            }
            else
                buttonPressed = true;
        }
    }


    private IEnumerator PlayDelayedSound()
    {
        yield return new WaitForSeconds(0.792f);  

        SignalLoop.Play();
        if (count == 2)
        {
            SignalLoop.Stop();
        }
    }
}