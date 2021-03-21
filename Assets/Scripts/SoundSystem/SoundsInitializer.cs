using UnityEngine;

public class SoundsInitializer : MonoBehaviour
{
    private void Awake() 
    {
        Sound[] sounds = Resources.LoadAll<Sound>("Sounds");
        GameObject audioSources = new GameObject("AudioSources");
        for(int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Source = audioSources.AddComponent<AudioSource>();
            sounds[i].Source.clip = sounds[i].Clip;
            sounds[i].Source.loop = sounds[i].Loop;
            sounds[i].Source.volume = sounds[i].Volume;
        }    
    }
}