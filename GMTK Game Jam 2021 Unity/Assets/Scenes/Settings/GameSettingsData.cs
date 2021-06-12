using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Settings", menuName = "Settings")]
public class GameSettingsData : ScriptableObject
{
    [SerializeField] private float masterVolume;
    [SerializeField] private float sfxVolume;
    [SerializeField] private float musicVolume;

    public float MasterVolume { get => this.masterVolume; set => this.masterVolume = value; }
    public float SfxVolume { get => this.sfxVolume; set => this.sfxVolume = value; }
    public float MusicVolume { get => this.musicVolume; set => this.musicVolume = value; }
}
