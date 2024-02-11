using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Back2button : MonoBehaviour
{
    public GameObject Back1Button;
    public GameObject Back2Button;
    public GameObject PedalsConfiguration;
    public GameObject ShifterConfiguration;
    public GameObject SteeringWheelConfiguration;
    public GameObject Driving_components;
    public void ButtonClick()
    {
        Back1Button.SetActive(true);
        Back2Button.SetActive(false);
        ShifterConfiguration.SetActive(false);
        SteeringWheelConfiguration.SetActive(false);
        PedalsConfiguration.SetActive(false);
        Driving_components.SetActive(true);
    }

}
