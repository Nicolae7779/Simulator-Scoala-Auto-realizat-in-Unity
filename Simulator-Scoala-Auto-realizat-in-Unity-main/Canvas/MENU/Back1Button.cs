using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Back1Button : MonoBehaviour
{
    public GameObject Controls;

    public GameObject DarkAndLight;

    public GameObject DrivingComponents;
    public void ButtonClick()
    {
        DarkAndLight.SetActive(false);
        DrivingComponents.SetActive(true);
        Controls.SetActive(false);
    }

}
