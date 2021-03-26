using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private Sound deathSound = null;
    [SerializeField] private Sound runningSound = null;
    [Header("Extinguisher")]
    [SerializeField] private Extinguisher extinguisher = null;
    [SerializeField] private Sound extinguishingSound = null;
    [Header("Jump")]
    [SerializeField] private Sound jumpSound = null;
    [SerializeField] private Sound jumpVoice = null;
    [SerializeField] [Range(0, 1)] private float jumpVoiceProbability = 0.5f;

    private PlayerController controller;
    private Player player;

    private void Awake() 
    {
        controller = GetComponent<PlayerController>();   
        player = GetComponent<Player>(); 
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

    private void PlayDeathSound() => deathSound.PlayRandomClip();

    private void PlayJumpSound()
    {
        jumpSound.Source.Play();
        if(Random.Range(0f, 1f) <= jumpVoiceProbability) jumpVoice.PlayRandomClip();
    }

    private void UpdateSound(Sound sound, bool condition)
    {
        if(condition)
        {
            if(!sound.Source.isPlaying) sound.Source.Play();
        }
        else sound.Source.Stop(); 
    }
}