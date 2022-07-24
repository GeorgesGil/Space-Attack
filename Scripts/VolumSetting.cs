using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;

public class VolumSetting : MonoBehaviour
{
    public AudioMixer mixer;
    public GameObject window;
    public Slider masterSlider;
    public Slider sfxSlider;
    public Slider musicSlider;

    private void Start()
    {
        //do we have saved Volume in playerprefs???
        if(PlayerPrefs.HasKey("MasterVolume"))
        {
            //set the mixer volume levels based on the saved playerprefs
            mixer.SetFloat("MasterVolume", PlayerPrefs.GetFloat("MasterVolume"));
            mixer.SetFloat("SFXVolume", PlayerPrefs.GetFloat("SFXVolume"));
            mixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("MusicVolume"));

            SetSliders();
        }
        else
        {
            //if we didnt save the value just set a default value
            SetSliders();
        }
    }

    private void Update()
    {
        //if we push the button V sound window will be get activated
        if(Input.GetKeyDown(KeyCode.V))
        {
            window.SetActive(!window.activeInHierarchy);
        }
    }

    //called at the start of the game
    //set the slider value to be the saved volume setting
    void SetSliders()
    {
        masterSlider.value = PlayerPrefs.GetFloat("MasterVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
    }

    public void UpdateMasterVolume()
    {
        //Update Value and save it to player prefs
        mixer.SetFloat("MasterVolume", masterSlider.value);
        PlayerPrefs.SetFloat("MasterVolume", masterSlider.value);
    }

    public void UpdateSFXVolume()
    {
        //Update Value and save it to player prefs
        mixer.SetFloat("SFXVolume", sfxSlider.value);
        PlayerPrefs.SetFloat("SFXVolume", sfxSlider.value);
    }

    public void UpdateMusicVolume()
    {
        //Update Value and save it to player prefs
        mixer.SetFloat("MusicVolume", musicSlider.value);
        PlayerPrefs.SetFloat("MusicVolume", musicSlider.value);
    }
}
