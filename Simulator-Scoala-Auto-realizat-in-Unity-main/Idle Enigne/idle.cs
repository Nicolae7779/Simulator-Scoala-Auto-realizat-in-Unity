using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class idle : MonoBehaviour
{
    //public AudioClip sunet; // sunetul pe care doriți să-l redați în mod repetat
    public AudioSource Idle;
    public AudioSource Idle2;


    public GameObject IdleDeselect;
    public GameObject StartEngineSelect;

    public bool Idle2Played = false;
    public bool Idle2RESET = false;

    public float count = 0f;
    public Text Rpm;

    public bool istrue = false;
    public bool IdleDeselect2 = false;

    public float Rpm2;
    public float Rpm0_1;

    public int RpmInScene;



    private void Awake()
    {
        Idle = GetComponent<AudioSource>();
        //audioSource.clip = sunet;
        Idle.loop = true; 
    }

    private void Update()
    {
        if (count <= 1)
        {
            count += Time.deltaTime;
            istrue = true;
        }
        if (istrue)
        {
            Idle.pitch = Mathf.Lerp(0f, 1f, count);
        }




        //float RpmRatio = Mathf.InverseLerp(0f, 6000f, RpmInScene);

        //if (Idle2RESET)
        //{
            if (int.TryParse(Rpm.text, out RpmInScene))
            {
                Rpm2 = RpmInScene / 6000f;
                if (Rpm2 >= 0.0833f && Rpm2 <= 0.1333f && count >= 1)
                {
                    Idle.Stop();

                    Rpm0_1 = Mathf.Clamp01((Rpm2 - 0.0833f) / (0.1333f - 0.0833f));

                    //Debug.Log(1f - Rpm0_1);

                    float pi = Mathf.Lerp(1f, 0f, 1f - Rpm0_1);
                    //Idle2.pitch = Mathf.Lerp(1f, 0f, 1f - Rpm0_1);

                    //Idle2.Play();
                    Idle2.pitch = pi;

                    if (!Idle2Played && Rpm0_1 < 1f)
                    {

                        Idle2.Play();
                        Idle2Played = true;

                        
                    }
                    else if (Rpm0_1 == 1f)
                    {
                        Idle2Played = false;
                        Idle2.Stop();
                    }
                    Debug.Log("Idle2Played este    " + Idle2Played);

                    //if (!Idle2Played)
                    //    Idle2.Stop();

                }
            }
        //}

        //if (Rpm0_1 >= 0.1f)
        //    IdleDeselect2 = true;

        //if (Rpm0_1 <= 0.09f && IdleDeselect2)
        //{
        //    Debug.Log("sssssssssssssssssssssssssssssssssssssss");
        //    IdleDeselect.SetActive(false);
        //    StartEngineSelect.SetActive(true);
        //    IdleDeselect2 = false;
        //}



        //if (RpmInScene <= 810)
        //    IdleDeselect2 = true;

        //if(IdleDeselect)
        //{
        //    if (RpmInScene <= 500)
        //        IdleDeselect.SetActive(false);
        //        IdleDeselect2 = false;
        //}


    }

    private void OnEnable()
    {
        //IdleDeselect2 = false;
        Idle.Play();
      //  Idle2RESET = true;
        //StartEngineSelect.SetActive(false);
    }

    private void OnDisable()
    {
        Idle.Stop();
        count = 0f;
        Idle2Played = false;
        Rpm2 = 0f;
        Rpm0_1 = 0f;

    }
}
