using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cubtime : MonoBehaviour
{
    private float touchTime;  // Timpul în care colizoarele se ating
    private bool isCounting;  // Indicator dacă colizoarele se ating sau nu

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("MeshColliderTag"))
        {
            touchTime = Time.time;  // Începe numărarea timpului la atingere
            isCounting = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("MeshColliderTag"))
        {
            isCounting = false;  // Oprește numărarea timpului la încetarea atingerii
        }
    }

    private void Update()
    {
        if (isCounting)
        {
            float elapsedTime = Time.time - touchTime;
            Debug.Log("Timpul de atingere: " + elapsedTime.ToString("F2") + " secunde");
        }
    }
}