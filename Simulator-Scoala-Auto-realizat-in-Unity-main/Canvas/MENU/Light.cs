using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light : MonoBehaviour
{
    public GameObject Menu;

    public GameObject CarsGarage;

    public GameObject GarageLight;

    internal float intensity;

    public void ButtonClick()
    {
        Menu.SetActive(false);

        CarsGarage.SetActive(true);

        GarageLight.SetActive(false);
    }
}
