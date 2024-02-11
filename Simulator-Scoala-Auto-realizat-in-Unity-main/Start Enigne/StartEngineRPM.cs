using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartEngineRPM : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData proprieties;

    //public GameObject StartEngine;
    //public GameObject IdleSound;

    public float EngineRPM;

    public Text RpmText;

    public float timer = 0f;

    public GameObject DeselectStartEngineRPM;

    private void Start()
    {
        print(LogitechGSDK.LogiSteeringInitialize(false));
    }

    private void OnEnable()
    {
        timer = 0f;
    }


    private void FixedUpdate()
    {
            timer += Time.deltaTime;


            EngineRPM = Mathf.Lerp(110f, 800f, timer);
            RpmText.text = EngineRPM.ToString("F0");

        if (timer >= 1)
        {
            DeselectStartEngineRPM.SetActive(false);
        }
    }






}
