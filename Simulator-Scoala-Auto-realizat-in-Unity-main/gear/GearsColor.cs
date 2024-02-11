using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Text))]
public class GearsColor : MonoBehaviour
{
    public Color N = Color.gray;
    public Color A = Color.white;
    public Color R = new Color(1f, 0.5f, 0f);

    private Text GearText;



    protected virtual void Start()
    {
        GearText = GetComponent<Text>();

        if (!GearText)
        {
            enabled = false;
        }
    }

    protected virtual void Update()
    {
        string trimmedText = GearText.text.Trim();
        if (float.TryParse(trimmedText, out float CurrentGear))
        {
            if (CurrentGear >= 1 && CurrentGear <= 6)
            {
                GearText.color = A;
            }
        }
        else if (trimmedText == "R")
        {
            GearText.color = R;
        }
        else if (trimmedText == "N")
        {
            GearText.color = N;
        }
    }
}



