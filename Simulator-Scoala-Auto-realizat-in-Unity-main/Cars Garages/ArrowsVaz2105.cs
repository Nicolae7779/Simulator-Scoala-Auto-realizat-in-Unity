using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowsVaz2105 : MonoBehaviour
{
    public GameObject Speedometer;
    public GameObject Arrows;

    void Update()
    {
        if (Speedometer.activeSelf)
        {
            Arrows.SetActive(false);
        }
    }
}
