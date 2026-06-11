using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeManager : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Slider volumeSlider;

    private void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MasterVolume", 0.8f);
        volumeSlider.value = savedVolume;
        SetVolume(savedVolume);
    }

    public void SetVolume(float sliderValue)
    {
        float decibelValue = Mathf.Log10(sliderValue) * 20;
        audioMixer.SetFloat("MasterVolume", decibelValue);
        PlayerPrefs.SetFloat("MasterVolume", sliderValue);
    }
}
