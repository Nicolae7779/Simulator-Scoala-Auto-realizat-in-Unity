using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Dark : MonoBehaviour
{
    public Material SkyMaterial;
    public GameObject LightInGarage;
    public GameObject L3;

    public GameObject Menu;

    public GameObject CarsGarage;

    public GameObject ParkLamp;

    public void ButtonClick()
    {
        RenderSettings.skybox = SkyMaterial;

        Menu.SetActive(false);

        CarsGarage.SetActive(true);

        ParkLamp.SetActive(true);

        RenderSettings.ambientIntensity = 0f;
        RenderSettings.reflectionIntensity = 0.2f;

        LightInGarage.SetActive(true);
        L3.SetActive(true);
    }
}






    
