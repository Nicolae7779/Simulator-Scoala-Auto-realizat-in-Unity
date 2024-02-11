using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FPS : MonoBehaviour
{
    [Header("Canvas")]
    public Canvas canvas;

    [Header("FPS")]

    public Text Unit;
    public Text Fps;


    private void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));

        Unit = canvas.GetComponentsInChildren<Text>()[1];
        Fps = canvas.GetComponentsInChildren<Text>()[2];

    }

    private void FixedUpdate()
    {
        Unit.enabled = true;
        Fps.enabled = true;
    }
}
