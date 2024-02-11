using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopEngine : MonoBehaviour
{
    public GameObject DeselectStopEngineSound;

    public float StopEngineTime = 2f;

    public float count = 0f;

    public AudioSource StopEngineSound;



    private void Start()
    {
        StopEngineSound.Play();
    }

    private void Update()
    {
       count += Time.deltaTime;

        if (count >= StopEngineTime)
        {
            count = 0f;
            DeselectStopEngineSound.SetActive(false);
        }
    }

}