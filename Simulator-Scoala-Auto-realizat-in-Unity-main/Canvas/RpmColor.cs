using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class RpmColor : MonoBehaviour
{
    public Color Green  = Color.green;
    public Color Yellow = Color.yellow;
    public Color Red    = Color.red;

    private Text RpmText;
       
    public float Perfect             = 3000f;
    public float Okay                = 5000f;
    public float Bad                 = 6000f;
    public float LowRpmEngineYellow  = 795f;
    public float LowRpmEngineRed     = 700f;



    protected virtual void Start()
    {
        RpmText = GetComponent<Text>();

        if (!RpmText)
        {
            enabled = false;
        }
    }

    protected virtual void Update()
    {
        if (RpmText.text != null && float.TryParse(RpmText.text, out float currentRPM))
        {
            if (currentRPM <= Perfect && currentRPM >= LowRpmEngineYellow)
            {
                RpmText.color = Green;
            }
            else if (currentRPM > Perfect && currentRPM < Okay)
            {
                RpmText.color = Yellow;
            }
            else if (currentRPM >= Okay && currentRPM <= Bad)
            {
                RpmText.color = Red;
            }
            else if (currentRPM >= LowRpmEngineRed && currentRPM < LowRpmEngineYellow)
            {
                RpmText.color = Yellow;
            }
            else if (currentRPM < LowRpmEngineRed)
            {
                RpmText.color = Red;
            }
        }
    }
}