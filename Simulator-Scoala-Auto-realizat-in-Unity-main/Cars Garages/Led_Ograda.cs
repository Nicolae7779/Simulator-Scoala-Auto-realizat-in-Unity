using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Led_Ograda : MonoBehaviour
{
    LogitechGSDK.LogiControllerPropertiesData properties;

    private int first = 0;

    private int third = 1;
    private int fourth = 6;


    public AudioSource source;


    private void Start()
    {
        LogitechGSDK.LogiPlayLeds(first, 2, third, fourth);

        source = GetComponent<AudioSource>();
    }
    void Update()
    {
        //Leds();
        if (LogitechGSDK.LogiUpdate() && LogitechGSDK.LogiIsConnected(0))
        {
            if (Input.GetKeyUp(KeyCode.A))
            {
                LogitechGSDK.LogiPlayLeds(first, 0, third, fourth);  //0    Leds OFF
            }

            /*
            else if (Input.GetKeyUp(KeyCode.S))
            {
                LogitechGSDK.LogiPlayLeds(first, 1, third, fourth);  //1    1, 10      first green led
            }
            else if (Input.GetKeyUp(KeyCode.D))
            {
                LogitechGSDK.LogiPlayLeds(first, 2, third, fourth);  //2    1,2  9,10      all green leds
            }
            else if (Input.GetKeyUp(KeyCode.F))
            {
                LogitechGSDK.LogiPlayLeds(first, 3, third, fourth);  //3    1,2,3  8,9,10   all green leds + first yellow led
            }
            else if (Input.GetKeyUp(KeyCode.G))
            {
                LogitechGSDK.LogiPlayLeds(first, 4, third, fourth);  //4    1,2,3,4  7,8,9,10  all  all green leds + all yellow leds
            }
            else if (Input.GetKeyUp(KeyCode.H))
            {
                LogitechGSDK.LogiPlayLeds(first, 5, third, fourth);  //5     1,2,3,4,5,6,7,8,9,10  all leds ON
            }
            else if (Input.GetKeyUp(KeyCode.J))
            {
                LogitechGSDK.LogiPlayLeds(first, 6, third, fourth);  //6     1,2,3,4,5,6,7,8,9,10  all leds FLASHING
            }
            */

        }
    }


    private void OnTriggerEnter(Collider col)
    {
        if (col.tag == "Green")
        {
            Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 2, third, fourth);
        }

        if (col.tag == "Yellow1")
        {
            Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 3, third, fourth);
        }

        if (col.tag == "Yellow2")
        {
            Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 4, third, fourth);
        }

        if (col.tag == "Red")
        {
            Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 5, third, fourth);
        }

        if (col.tag == "Red2")
        {
            Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 5, third, fourth);
        }
    }

    private void OnTriggerExit(Collider col)
    {
        if (col.tag == "Green")
        {
            //Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 2, third, fourth);
        }
        if (col.tag == "Yellow1")
        {
            //Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 2, third, fourth);
        }

        if (col.tag == "Yellow2")
        {
            Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 3, third, fourth);
        }

        if (col.tag == "Red")
        {
            Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 4, third, fourth);
        }

        if (col.tag == "Black")
        {
            Debug.Log("Collision Detected");
            LogitechGSDK.LogiPlayLeds(first, 0, third, fourth);
        }
    }


    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Wall")
        {
            Debug.Log("wall");

            source.Play();
        }
    }
}
