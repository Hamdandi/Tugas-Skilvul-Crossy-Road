using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{

    [SerializeField] AudioMixer audioMixer;
    private static AudioManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(transform.gameObject);
        }
    }


    public void SetBGMVolume(float sliderValue)
    {
        audioMixer.SetFloat("BGM_VOL", Mathf.Log10(sliderValue) * 20);
    }

    public void SetSFXVolume(float sliderValue)
    {
        audioMixer.SetFloat("SFX_VOL", Mathf.Log10(sliderValue) * 20);
    }

    public void Mute(bool isMute)
    {
        if (isMute)
        {
            audioMixer.SetFloat("BGM_VOL", -80);
            audioMixer.SetFloat("SFX_VOL", -80);
        }
        else
        {
            audioMixer.SetFloat("BGM_VOL", 0);
            audioMixer.SetFloat("SFX_VOL", 0);
        }
    }

}

