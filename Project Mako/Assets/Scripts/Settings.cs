using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class Settings : MonoBehaviour
{
    [SerializeField] private AudioMixer mixer;
    private const string exposedGeneralParameterName = "masterVolume";
    private const string exposedSfxParameterName = "sfxVolume";
    private const string exposedMusicVolumeParameterName = "musicVolume";
    [SerializeField] private Slider generalVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    // Start is called before the first frame update
    void Start()
    {
        //mixer.FindMatchingGroups()
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void ConfigureGeneralVolume()
    {
        mixer.SetFloat(exposedGeneralParameterName, Mathf.Log(generalVolumeSlider.value) * 20f);
    }
    public void ConfigureSFXVolume()
    {
        mixer.SetFloat(exposedSfxParameterName, Mathf.Log(sfxVolumeSlider.value) * 20f);
    }
    public void ConfigureMusicVolume()
    {
        mixer.SetFloat(exposedMusicVolumeParameterName, Mathf.Log(musicVolumeSlider.value) * 20f);
    }
}
