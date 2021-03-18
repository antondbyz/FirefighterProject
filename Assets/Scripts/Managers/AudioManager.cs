using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private void Awake() 
    {
        Sound[] sounds = Resources.LoadAll<Sound>("Sounds");
        for(int i = 0; i < sounds.Length; i++)
        {
            sounds[i].Source = gameObject.AddComponent<AudioSource>();
            sounds[i].Source.clip = sounds[i].Clip;
            sounds[i].Source.loop = sounds[i].Loop;
            sounds[i].Source.volume = sounds[i].Volume;
        }    
    }
}