using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carspeed : MonoBehaviour
{
    public float speedCar;

    private void Start()
    {
        
    }

    void Update()
    {
        StartCoroutine(CalculateSpeed());
    }

    IEnumerator CalculateSpeed()
    {
        Vector3 lastPosition = transform.position;
        yield return new WaitForFixedUpdate();
        speedCar = (lastPosition - transform.position).magnitude / Time.deltaTime;
    }
}
