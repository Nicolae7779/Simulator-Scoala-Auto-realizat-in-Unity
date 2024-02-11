using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RPM : MonoBehaviour
{
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    public RectTransform arrow;

    public Text Rpm;
    public float CurrentRPM;

    private void Update()
    {
        float.TryParse(Rpm.text, out CurrentRPM);

        arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, CurrentRPM / 6000));

    }
}
