using UnityEngine;
using UnityEngine.UI;

public class Settings : MonoBehaviour
{
    public static Settings Instance;
    public static float MusicVolume = 1;
    public static float SoundsVolume = 1;
    
    public event System.Action<float> MusicVolumeChanged;
    public event System.Action<float> SoundsVolumeChanged;

    [SerializeField] private Slider musicVolumeSlider = null;
    [SerializeField] private Slider soundsVolumeSlider = null;

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        musicVolumeSlider.value = MusicVolume;
        soundsVolumeSlider.value = SoundsVolume;
        musicVolumeSlider.onValueChanged.AddListener((float value) => 
        {
            MusicVolume = value;
            MusicVolumeChanged?.Invoke(MusicVolume);
        });    
        soundsVolumeSlider.onValueChanged.AddListener((float value) => 
        {
            SoundsVolume = value;
            SoundsVolumeChanged?.Invoke(SoundsVolume);
        }
        );    
    }
}