using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private Extinguisher extinguisher = null;
    [SerializeField] private Sound[] deathSounds = null;
    [SerializeField] private Sound runningSound = null;
    [SerializeField] private Sound extinguishingSound = null;

    private PlayerController controller;
    private Player player;

    private void Awake() 
    {
        controller = GetComponent<PlayerController>();   
        player = GetComponent<Player>(); 
    }

    private void OnEnable() => player.Died += PlayRandomDeathSound;

    private void OnDisable() 
    {
        player.Died -= PlayRandomDeathSound;
        if(runningSound.Source != null) runningSound.Source.Stop();    
        if(extinguishingSound.Source != null) extinguishingSound.Source.Stop();
    }

    private void Update() 
    {
        UpdateSound(runningSound, controller.NewVelocity.x != 0 && controller.IsGrounded && !GameController.Instance.IsPaused);
        UpdateSound(extinguishingSound, extinguisher.IsTurnedOn);
    }

    private void PlayRandomDeathSound() => deathSounds[Random.Range(0, deathSounds.Length)].Source.Play();

    private void UpdateSound(Sound sound, bool condition)
    {
        if(condition)
        {
            if(!sound.Source.isPlaying) sound.Source.Play();
        }
        else sound.Source.Stop(); 
    }
}