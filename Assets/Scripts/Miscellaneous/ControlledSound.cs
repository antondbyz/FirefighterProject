using UnityEngine;

[CreateAssetMenu(menuName = "My assets/New controlled sound")]
public class ControlledSound : ScriptableObject 
{
    [HideInInspector] public AudioSource Source;

    [SerializeField] private AudioClip clip = null;
    [SerializeField] private bool loop = false;

    public void Initialize(GameObject audioSources)
    {
        Source = audioSources.AddComponent<AudioSource>();
        Source.clip = clip;
        Source.loop = loop;
    }
}