using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioMixer mixer;

    public AudioSource[] sounds;

    public Slider music;
    public Slider sfx;

    private void Awake() {
        instance = this;
    }

    public void PlayEffectSound(int id)
    {
        sounds[id].Play();
    }

    public void reload()
    {
        music.value = PlayerData.getData().music;
        sfx.value = PlayerData.getData().sfx;
    }

    public void SetMusicVolume(float value)
    {
        if (value < 0.001 || value > 1)
            return;
        mixer.SetFloat("MusicVolume", Mathf.Log10(value)*20);
        PlayerData.getData().music = value;
    }

    public void setSFXVolume(float value)
    {
        if (value < 0.001 || value > 1)
            return;
        mixer.SetFloat("SFXVolume", Mathf.Log10(value) *20);
        PlayerData.getData().sfx = value;
    }

    public void SaveParameterSound()
    {
        PlayerData.getData().database.SaveData();
    }
}
