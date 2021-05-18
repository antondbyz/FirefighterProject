using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static float MusicVolume 
    {
        get => musicVolume;
        set
        {
            musicVolume = Mathf.Clamp01(value);
            MusicVolumeChanged?.Invoke(musicVolume);
        }
    }
    public static float SoundsVolume 
    {
        get => soundsVolume;
        set
        {
            soundsVolume = Mathf.Clamp01(value);
            SoundsVolumeChanged?.Invoke(soundsVolume);
        }
    }
    public static System.Action<float> MusicVolumeChanged;
    public static System.Action<float> SoundsVolumeChanged;

    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Slider soundsVolumeSlider = null;

    private static float musicVolume = 1;
    private static float soundsVolume = 1;

    private void Awake() 
    {
        musicVolumeSlider.value = musicVolume;
        soundsVolumeSlider.value = soundsVolume;
        musicVolumeSlider.onValueChanged.AddListener((float value) => MusicVolume = value);    
        soundsVolumeSlider.onValueChanged.AddListener((float value) => SoundsVolume = value);    
    }
}