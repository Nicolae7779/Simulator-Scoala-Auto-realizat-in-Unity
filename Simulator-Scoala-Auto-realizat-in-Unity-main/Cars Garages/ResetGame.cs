using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ResetGame : MonoBehaviour
{
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


    void butoane_volan(LogitechGSDK.DIJOYSTATE2ENGINES v_butoane)
    {




        for (int i = 0; i < 128; i++)
        {
            if (v_butoane.rgbButtons[i] == 128)
            {
                if (i == 24)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                }
            }
        }
    }
}
