using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public float minSpeedArrowAngle;
    public float maxSpeedArrowAngle;

    public RectTransform arrow;

    public Text Speed;
    public float CurrentSpeed;

    private void Update()
    {
        float.TryParse(Speed.text, out CurrentSpeed);

        arrow.localEulerAngles = new Vector3(0, 0, Mathf.Lerp(minSpeedArrowAngle, maxSpeedArrowAngle, CurrentSpeed / 260));

    }
}
