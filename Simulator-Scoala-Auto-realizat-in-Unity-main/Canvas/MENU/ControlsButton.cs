using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlsButton : MonoBehaviour
{
    public GameObject Controls;
    public void ButtonClick()
    {
        Controls.SetActive(true);
    }

}
