using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private Sound deathSound = null;
    [SerializeField] private Sound runningSound = null;
    [SerializeField] private Sound jumpSound = null;
    [SerializeField] private Sound extinguishingSound = null;

    private PlayerController controller;
    private Player player;
    private Extinguisher extinguisher;

    private void Awake() 
    {
        controller = GetComponent<PlayerController>();   
        player = GetComponent<Player>();
        extinguisher = GetComponentInChildren<Extinguisher>(); 
    }

    private void OnEnable() 
    { 
        player.Died += PlayDeathSound;
        controller.Jumped += PlayJumpSound;
    }

    private void OnDisable() 
    {
        player.Died -= PlayDeathSound;
        controller.Jumped -= PlayJumpSound;
        if(runningSound.Source != null) runningSound.Source.Stop();    
        if(extinguishingSound.Source != null) extinguishingSound.Source.Stop();
    }

    private void Update() 
    {
        UpdateSound(runningSound, controller.NewVelocity.x != 0 && controller.IsGrounded && !GameController.Instance.IsPaused);
        UpdateSound(extinguishingSound, extinguisher.IsTurnedOn && !GameController.Instance.IsPaused);
    }

    private void PlayDeathSound() => deathSound.Play();

    private void PlayJumpSound() => jumpSound.Play();

    private void UpdateSound(Sound sound, bool condition)
    {
        if(condition)
        {
            if(!sound.Source.isPlaying) sound.Play();
        }
        else sound.Source.Stop(); 
    }
}