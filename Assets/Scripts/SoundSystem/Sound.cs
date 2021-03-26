using UnityEngine;

[CreateAssetMenu(menuName = "My assets/New sound")]
public class Sound : ScriptableObject 
{
    [HideInInspector] public AudioSource Source;

    [SerializeField] private AudioClip[] clips;
    [SerializeField] private bool loop;
    [SerializeField] [Range(0, 1)] private float volume = 1;

    public void Initialize(GameObject audioSources)
    {
        Source = audioSources.AddComponent<AudioSource>();
        Source.clip = clips[0];
        Source.loop = loop;
        Source.volume = volume;
    }

    public void PlayRandomClip()
    {
        Source.clip = clips[Random.Range(0, clips.Length)];
        Source.Play();
    }
}