using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.EventSystems;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    void OnEnable()
    {
        EventSystem eventSystem = EventSystem.current;
        GameObject backButton = GameObject.Find("back");
        eventSystem.SetSelectedGameObject(backButton);
    }

    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }
}
