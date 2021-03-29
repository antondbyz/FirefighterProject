using UnityEngine;

[CreateAssetMenu(menuName = "My assets/New sound")]
public class Sound : ScriptableObject 
{
    [HideInInspector] public AudioSource Source;

    [SerializeField] private AudioClip[] clips;
    [SerializeField] private bool loop;

    public void Initialize(GameObject audioSources)
    {
        Source = audioSources.AddComponent<AudioSource>();
        Source.clip = clips[0];
        Source.loop = loop;
    }

    public void Play()
    {
        if(clips.Length > 1) Source.clip = clips[Random.Range(0, clips.Length)];
        Source.Play();
    }
}