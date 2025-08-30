using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public AudioMixer audioMixer;

    public Slider musicSlider;
    public Slider sfxSlider;

    [Header("UI Panels")]
    public CanvasGroup optionsCanvas;

    private bool optionsOpen = false;

    private void Start()
    {
        LoadVolume();
        MusicManager.Instance.PlayMusic("Track1");
    }

    // ======== Buttons =========
    public void PlayGame()
    {
        SoundManager.Instance.PlaySound2D("StartGame");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        MusicManager.Instance.PlayMusic("Track1");
    }

    public void Options()
    {
        SoundManager.Instance.PlaySound2D("DigitalClick");
        optionsOpen = !optionsOpen;

        optionsCanvas.alpha = optionsOpen ? 1 : 0;
        optionsCanvas.interactable = optionsOpen;
        optionsCanvas.blocksRaycasts = optionsOpen;

        Debug.Log("Options Panel " + (optionsOpen ? "Opened" : "Closed"));

    }

    public void QuitGame()
    {
        SoundManager.Instance.PlaySound2D("DigitalClick");
        Application.Quit();
    }


    //=========Volume=========

    public void UpdateMusicVolume(float volume)
    {
        audioMixer.SetFloat("MusicVolume", volume);
    }

    public void UpdateSoundVolume(float volume)
    {
        audioMixer.SetFloat("SFXVolume", volume);
    }

    public void SaveVolume()
    {
        audioMixer.GetFloat("MusicVolume", out float musicVolume);
        PlayerPrefs.SetFloat("MusicVolume", musicVolume);

        audioMixer.GetFloat("SFXVolume", out float sfxVolume);
        PlayerPrefs.SetFloat("SFXVolume", sfxVolume);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("MusicVolume");
        sfxSlider.value = PlayerPrefs.GetFloat("SFXVolume");
    }
}
