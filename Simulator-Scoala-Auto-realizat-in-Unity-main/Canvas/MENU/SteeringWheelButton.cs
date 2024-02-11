using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SteeringWheelButton : MonoBehaviour
{
    public GameObject Back1Buton;
    public GameObject Back2Buton;
    public GameObject DrivingComponents;
    public GameObject SteeringWheelConfiguration;
    public void ButtonClick()
    {
        Back1Buton.SetActive(false);
        Back2Buton.SetActive(true);
        DrivingComponents.SetActive(false);
        SteeringWheelConfiguration.SetActive(true);
    }
}
