using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class PlayButton : MonoBehaviour
{

    public GameObject Controls;

    public GameObject DarkAndLight;

    public GameObject DrivingComponents;

    public void ButtonClick()
    {
        Controls.SetActive(true);

        DarkAndLight.SetActive(true);
        DrivingComponents.SetActive(false);

    }
}
