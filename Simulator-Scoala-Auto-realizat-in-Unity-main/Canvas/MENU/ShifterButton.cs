using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShifterButton : MonoBehaviour
{
    public GameObject Back1Buton;
    public GameObject Back2Buton;
    public GameObject DrivingComponents;
    public GameObject ShifterConfiguration;
    public void ButtonClick()
    {
        Back1Buton.SetActive(false);
        Back2Buton.SetActive(true);
        DrivingComponents.SetActive(false);
        ShifterConfiguration.SetActive(true);
    }
}
