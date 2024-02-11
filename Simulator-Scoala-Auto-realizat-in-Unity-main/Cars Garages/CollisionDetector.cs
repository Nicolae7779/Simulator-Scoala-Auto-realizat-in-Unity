using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    public GameObject ObstacleDetection;

    public float count = 0f;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Car"))
        {
            ObstacleDetection.SetActive(true);
        }
    }
    private void FixedUpdate()
    {
        if (ObstacleDetection.activeSelf)
        {
            count += Time.deltaTime;

            if (count > 3)
            {
                ObstacleDetection.SetActive(false);
            }
        }
        else
        {
            count = 0;
        }
    }
}
