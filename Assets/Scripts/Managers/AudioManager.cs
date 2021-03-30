using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    private AudioSource audioSource;

    public void PlayClip(AudioClip clip) => audioSource.PlayOneShot(clip);

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);
        GameObject audioSources = new GameObject("AudioSources");
        audioSource = audioSources.AddComponent<AudioSource>();
        ControlledSound[] sounds = Resources.LoadAll<ControlledSound>("ControlledSounds");
        for(int i = 0; i < sounds.Length; i++) sounds[i].Initialize(audioSources);
    }
}