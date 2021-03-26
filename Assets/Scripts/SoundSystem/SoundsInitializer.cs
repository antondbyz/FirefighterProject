using UnityEngine;

public class SoundsInitializer : MonoBehaviour
{
    private void Awake() 
    {
        Sound[] sounds = Resources.LoadAll<Sound>("Sounds");
        GameObject audioSources = new GameObject("AudioSources");
        for(int i = 0; i < sounds.Length; i++) sounds[i].Initialize(audioSources);
    }
}