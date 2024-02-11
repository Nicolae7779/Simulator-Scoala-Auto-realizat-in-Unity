using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PITCH : MonoBehaviour
{

    public float maxPitch = 3f;
    private float count = 0f;



    //public AudioClip Sound; // sunetul pe care doriți să-l redați în mod repetat
    public AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
        //audioSource.clip = Sound;
        audioSource.loop = true; // setăm sunetul să fie reprodus în mod repetat
    }

    private void Update()
    {
        count += Time.deltaTime / 10;
        float newPitch = Mathf.Lerp(1f, maxPitch, count);

        // Set the new pitch to the AudioSource
        audioSource.pitch = newPitch;
    }

    private void OnEnable()
    {
        audioSource.Play(); // începem redarea sunetului când GameObject-ul devine activ
      

        // Calculate the new pitch based on the count
 
    }

    private void OnDisable()
    {
        audioSource.Stop(); // oprim redarea sunetului când GameObject-ul devine inactiv
    }

    //public float pitchIncreaseRate = 0.1f;

}


/*
 * 
 * 
 * */
