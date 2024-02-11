using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class L3 : MonoBehaviour
{
    public GameObject ButtonL3;
    public GameObject LightON;

    void Update()
    {

        if (LightON.activeSelf)
        {
            ButtonL3.SetActive(true);
        }
        else
        {
            ButtonL3.SetActive(false);
        }
    }
}
