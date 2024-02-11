using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetRotationOnDeselect : MonoBehaviour
{
    private Quaternion initialRotation;

    private void Start()
    {
        // Salvăm rotatia inițială a obiectului la începutul jocului
        initialRotation = transform.rotation;
    }

    private void OnDisable()
    {
        // Resetăm rotatia la valoarea inițială atunci când obiectul este deselectat
        transform.rotation = initialRotation;
    }
}
