using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioMixer mixer;

    public AudioSource[] sounds;

    private void Awake() {
        instance = this;
    }

    public void PlayEffectSound(int id)
    {
        sounds[id].Play();
    }

    public void SetMusicVolume(float value){
        mixer.SetFloat("Music", Mathf.Log10(value)*20);
    }

    public void setSFXVolume(float value){
        mixer.SetFloat("SFX", Mathf.Log10(value)*20);
    }
}
