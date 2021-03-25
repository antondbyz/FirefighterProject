using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class CustomAudioSettings : MonoBehaviour
{
    [SerializeField] private bool pauseWithGame = true;
    [SerializeField] private bool alignWithAudioListener = true;

    private static Transform audioListener;
    private AudioSource audioSource;

    private void Awake() 
    {
        if(alignWithAudioListener)
        {
            if(audioListener == null) audioListener = GameObject.FindObjectOfType<AudioListener>().transform;
            Transform myTransform = transform;
            myTransform.position = new Vector3(myTransform.position.x, myTransform.position.y, audioListener.position.z); 
        }
        audioSource = GetComponent<AudioSource>();      
    }   

    private void OnEnable() 
    {
        if(pauseWithGame)
        {
            GameController.Instance.GamePaused += PauseAudio;
            GameController.Instance.GameUnpaused += UnpauseAudio;
        }
    }

    private void OnDisable() 
    {
        GameController.Instance.GamePaused -= PauseAudio;
        GameController.Instance.GameUnpaused -= UnpauseAudio;
    } 

    private void PauseAudio() => audioSource.Pause();

    private void UnpauseAudio() => audioSource.UnPause();
}