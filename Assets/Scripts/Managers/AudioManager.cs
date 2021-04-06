using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    public void PlayClip(AudioClip clip) => audioSource.PlayOneShot(clip);

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Debug.LogWarning("More than one instance of AudioManager!");
        audioSource = GetComponent<AudioSource>();
    }
}