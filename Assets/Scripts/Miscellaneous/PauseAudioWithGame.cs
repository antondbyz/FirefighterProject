using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class PauseAudioWithGame : MonoBehaviour
{
    private AudioSource audioSrc;

    private void Awake() 
    {
        audioSrc = GetComponent<AudioSource>();       
    }   

    private void OnEnable() 
    {
        GameController.Instance.GamePaused += PauseAudio;
        GameController.Instance.GameUnpaused += UnpauseAudio;
    }

    private void OnDisable() 
    {
        GameController.Instance.GamePaused -= PauseAudio;
        GameController.Instance.GameUnpaused -= UnpauseAudio;
    } 

    private void PauseAudio() => audioSrc.Pause();

    private void UnpauseAudio() => audioSrc.UnPause();
}