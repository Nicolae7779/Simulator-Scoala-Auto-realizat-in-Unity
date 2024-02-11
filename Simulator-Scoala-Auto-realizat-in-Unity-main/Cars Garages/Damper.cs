using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damper : MonoBehaviour
{

    public int count = 0;

    public int power = 50;
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
                if (i == 10 && !buttonPressed)
                {
                    if (count < 2)
                        count++;
                }
                buttonPressed1 = true;

                if (count == 1)
                {
                   //if (LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_DAMPER))
                   //{
                   //    LogitechGSDK.LogiStopDamperForce(0);
                   //}
                   //else
                   //{
                       LogitechGSDK.LogiPlayDamperForce(0, power);
                   //}
                }
                else if (count == 2)
                {
                    //if (LogitechGSDK.LogiIsPlaying(0, LogitechGSDK.LOGI_FORCE_DAMPER))
                    //{
                    //    LogitechGSDK.LogiStopDamperForce(0);
                    //}
                    //else
                    //{
                        LogitechGSDK.LogiPlayDamperForce(0, 0);
                    //}
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
