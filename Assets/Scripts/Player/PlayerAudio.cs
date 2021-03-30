using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] deathClips = null;
    [SerializeField] private AudioClip jumpClip = null;
    [SerializeField] private AudioClip keyCollectedClip = null;
    [SerializeField] private AudioClip victimSavedClip = null;
    [SerializeField] private ControlledSound runningSound = null;
    [SerializeField] private ControlledSound extinguishingSound = null;

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
        player.Died += PlayRandomDeath;
        player.KeyCollected += PlayKeyCollected;
        player.VictimSaved += PlayVictimSaved;
        controller.Jumped += PlayJump;
    }

    private void OnDisable() 
    {
        player.Died -= PlayRandomDeath;
        player.KeyCollected -= PlayKeyCollected;
        player.VictimSaved -= PlayVictimSaved;
        controller.Jumped -= PlayJump;
        if(runningSound.Source != null) runningSound.Source.Stop();    
        if(extinguishingSound.Source != null) extinguishingSound.Source.Stop();
    }

    private void Update() 
    {
        UpdateSound(runningSound, controller.NewVelocity.x != 0 && controller.IsGrounded && !GameController.Instance.IsPaused);
        UpdateSound(extinguishingSound, extinguisher.IsTurnedOn && !GameController.Instance.IsPaused);
    }

    private void PlayRandomDeath() => AudioManager.Instance.PlayClip(deathClips[Random.Range(0, deathClips.Length)]);

    private void PlayKeyCollected() => AudioManager.Instance.PlayClip(keyCollectedClip);

    private void PlayVictimSaved() => AudioManager.Instance.PlayClip(victimSavedClip);

    private void PlayJump() => AudioManager.Instance.PlayClip(jumpClip);

    private void UpdateSound(ControlledSound sound, bool condition)
    {
        if(condition)
        {
            if(!sound.Source.isPlaying) sound.Source.Play();
        }
        else sound.Source.Stop(); 
    }
}