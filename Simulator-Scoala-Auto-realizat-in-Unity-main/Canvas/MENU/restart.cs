using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class restart : MonoBehaviour
{
    public void LoadScene(string SampleScene)
    {
        //Debug.Log("SampleScene");
        SceneManager.LoadScene(SampleScene);

    }
}
