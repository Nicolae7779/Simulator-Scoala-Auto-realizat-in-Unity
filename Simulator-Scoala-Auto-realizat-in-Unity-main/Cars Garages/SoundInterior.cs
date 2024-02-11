using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundInterior : MonoBehaviour
{
    public AudioSource StartEngineSound;
    public AudioSource IdleSound;
    public AudioSource Idle2Sound;
    public AudioSource StopEngineSound;
    public AudioSource AcceleratorSound;
    public AudioSource RpmSound2;

    public float StartEngineEnable;
    public float IdleEnable;
    public float Idle2Enable;
    public float StopEngineEnable;
    public float AcceleratorEnable;
    public float RpmEnable;

    public float StartEngineDisable;
    public float IdleDisable;
    public float Idle2Disable;
    public float StopEngineDisable;
    public float AcceleratorDisable;
    public float RpmDisable;


    private void Update()
    {

    }

    private void SetSoundVolume(AudioSource sound, float volume)
    {
        sound.volume = volume;
    }


    private void OnEnable()
    {
        SetSoundVolume(StartEngineSound, StartEngineEnable);
        SetSoundVolume(IdleSound, IdleEnable);
        SetSoundVolume(Idle2Sound, Idle2Enable);
        SetSoundVolume(StopEngineSound, StopEngineEnable);
        SetSoundVolume(AcceleratorSound, AcceleratorEnable);
        SetSoundVolume(RpmSound2, RpmEnable);
    }

    private void OnDisable()
    {
        SetSoundVolume(StartEngineSound, StartEngineEnable);
        SetSoundVolume(IdleSound, IdleDisable);
        SetSoundVolume(Idle2Sound, Idle2Disable);
        SetSoundVolume(StopEngineSound, StopEngineDisable);
        SetSoundVolume(AcceleratorSound, AcceleratorDisable);
        SetSoundVolume(RpmSound2, RpmDisable);
    }


}
