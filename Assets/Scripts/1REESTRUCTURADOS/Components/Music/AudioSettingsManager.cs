using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioSettingsManager : MonoBehaviour
{
    public AudioMixer audioMixer;

    public Slider sliderMaster;
    public Slider sliderMusic;
    public Slider sliderSFX;

    private void Start()
    {
        sliderMaster.onValueChanged.AddListener(SetMasterVolume);
        sliderMusic.onValueChanged.AddListener(SetMusicVolume);
        sliderSFX.onValueChanged.AddListener(SetSFXVolume);

    }

    public void SetMasterVolume(float value)
    {
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(value) * 20);
    }

    public void SetMusicVolume(float value)
    {
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(value) * 20);
    }

    public void SetSFXVolume(float value)
    {
        audioMixer.SetFloat("SFXVolume", Mathf.Log10(value) * 20);
    }
}
