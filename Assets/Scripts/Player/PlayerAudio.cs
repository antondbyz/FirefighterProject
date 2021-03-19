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
        if(controller.NewVelocity.x != 0 && controller.IsGrounded && !GameController.Instance.IsPaused)
        {
            if(!runningSound.Source.isPlaying) runningSound.Source.Play();
        }
        else runningSound.Source.Stop();    
        if(extinguisher.IsTurnedOn) 
        {
            if(!extinguishingSound.Source.isPlaying) extinguishingSound.Source.Play();
        }
        else extinguishingSound.Source.Stop();
    }

    private void PlayRandomDeathSound() => deathSounds[Random.Range(0, deathSounds.Length)].Source.Play();
}