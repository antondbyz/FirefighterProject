using UnityEngine;

[CreateAssetMenu(menuName = "My assets/New sound")]
public class Sound : ScriptableObject 
{
    public AudioClip Clip;
    public bool Loop;
    [Range(0, 1)] public float Volume = 1;    
    [HideInInspector] public AudioSource Source;
}