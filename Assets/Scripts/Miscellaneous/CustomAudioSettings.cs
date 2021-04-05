using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CustomAudioSettings : MonoBehaviour
{
    private enum AudioType { Music, Sound }

    [SerializeField] private AudioType type = AudioType.Sound;
    [SerializeField] private bool pauseWithGame = true;
    [SerializeField] private bool is3D = true;

    private static Transform audioListener;
    private AudioSource audioSource;

    private void Awake() 
    {
        if(is3D)
        {
            if(audioListener == null) audioListener = GameObject.FindObjectOfType<AudioListener>().transform;
            Transform myTransform = transform;
            myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, audioListener.position.z); 
        }
        audioSource = GetComponent<AudioSource>();      
        audioSource.volume = type == AudioType.Music ? Settings.MusicVolume : Settings.SoundsVolume;
        if(Settings.Instance != null) 
        {
            if(type == AudioType.Music) Settings.Instance.MusicVolumeChanged += (float volume) => audioSource.volume = volume;
            else Settings.Instance.SoundsVolumeChanged += (float volume) => audioSource.volume = volume;
        }
    }   

    private void OnEnable() 
    {
        if(GameController.Instance != null && pauseWithGame)
        {
            GameController.Instance.GamePaused += audioSource.Pause;
            GameController.Instance.GameUnpaused += audioSource.UnPause;
        }
    }

    private void OnDisable() 
    {
        if(GameController.Instance != null)
        {
            GameController.Instance.GamePaused -= audioSource.Pause;
            GameController.Instance.GameUnpaused -= audioSource.UnPause;
        }
    } 
}