using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioMixer mixer;

    public AudioSource source;

    public AudioClip[] sounds;

    private void Awake() {
        instance = this;
    }

    public void playSound(){
        AudioClip clip = sounds[0];
        source.PlayOneShot(clip);
    }

    public void SetMusicVolume(float value){
        mixer.SetFloat("MusicVolume", Mathf.Log10(value)*20);
    }

    public void setSFXVolume(float value){
        mixer.SetFloat("SFXVolume", Mathf.Log10(value)*20);
    }
}
