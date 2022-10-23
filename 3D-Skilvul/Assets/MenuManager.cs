using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Toggle toggleMute;
    [SerializeField] Slider sliderBGM;
    [SerializeField] Slider sliderSFX;


    public void Mute()
    {
        if (toggleMute.isOn)
        {
            sliderBGM.interactable = false;
            sliderSFX.interactable = false;
            Debug.Log("Volume : Mute");
        }
        else
        {
            sliderBGM.interactable = true;
            sliderSFX.interactable = true;
            Debug.Log("Volume : Unmute");
        }
    }

    public void BGMVolume(float sliderValue)
    {
        var musicValue = Mathf.FloorToInt(sliderValue * 100);
        Debug.Log("Music Volume : " + musicValue);
    }
    public void SFXVolume(float sliderValue)
    {
        var sfxValue = Mathf.FloorToInt(sliderValue * 100);
        Debug.Log("Music Volume : " + sfxValue);
    }

}
