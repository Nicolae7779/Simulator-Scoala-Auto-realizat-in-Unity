using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogitechG29 : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData proprieties;

    public GameObject SteeringWheel;

    void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
    }

    void Update()
    {
        if (!LogitechGSDK.LogiIsConnected(0))
        {
            SteeringWheel.SetActive(true);
        }
        else
        {
            SteeringWheel.SetActive(false);
        }
    }
}
