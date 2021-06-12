using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingBehaviour : MonoBehaviour
{
    [SerializeField] private GameSettingsData gameSettingsData;

    [SerializeField] private Slider masterSilder;
    [SerializeField] private Slider sfxSilder;
    [SerializeField] private Slider musicSilder;
    FMOD.Studio.Bus busMaster;
    FMOD.Studio.Bus busSFX;
    FMOD.Studio.Bus busMusic;

    private void Awake()
    {
        SetSettingsToCurrentValues();
        busMaster = FMODUnity.RuntimeManager.GetBus("bus:/Master");
        busSFX = FMODUnity.RuntimeManager.GetBus("bus:/Master/SFX");
        busMusic = FMODUnity.RuntimeManager.GetBus("bus:/Master/Music");
    }

    public void OnMasterVolumeChanged(float volume)
    {
        gameSettingsData.MasterVolume = volume;
        Debug.Log("settings master to " + gameSettingsData.MasterVolume);
        //change volume of master
        busMaster.setVolume(volume);
    }
    public void OnSfxVolumeChanged(float volume)
    {
        gameSettingsData.SfxVolume = volume;
        Debug.Log("settings sfx to " + gameSettingsData.SfxVolume);
        //change volume of sfx
        busSFX.setVolume(volume);
    }
    public void OnMusicVolumeChanged(float volume)
    {
        gameSettingsData.MusicVolume = volume;
        Debug.Log("settings music to " + gameSettingsData.MusicVolume);
        //change volume of music
        busMusic.setVolume(volume);
    }

    private void SetSettingsToCurrentValues()
    {
        this.masterSilder.value = this.gameSettingsData.MasterVolume;
        this.sfxSilder.value = this.gameSettingsData.SfxVolume;
        this.musicSilder.value = this.gameSettingsData.MusicVolume;

        //Change volume of music sfx and master here to gameSettingsData
    }
}
