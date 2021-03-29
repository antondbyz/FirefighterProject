using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private Sound deathSound = null;
    [SerializeField] private Sound runningSound = null;
    [SerializeField] private Sound jumpSound = null;
    [SerializeField] private Sound extinguishingSound = null;
    [SerializeField] private Sound keyCollectedSound = null;
    [SerializeField] private Sound victimSaved = null;

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
        player.Died += deathSound.Play;
        player.KeyCollected += keyCollectedSound.Play;
        player.VictimSaved += victimSaved.Play;
        controller.Jumped += jumpSound.Play;
    }

    private void OnDisable() 
    {
        player.Died -= deathSound.Play;
        player.KeyCollected -= keyCollectedSound.Play;
        player.VictimSaved -= victimSaved.Play;
        controller.Jumped -= jumpSound.Play;
        if(runningSound.Source != null) runningSound.Source.Stop();    
        if(extinguishingSound.Source != null) extinguishingSound.Source.Stop();
    }

    private void Update() 
    {
        UpdateSound(runningSound, controller.NewVelocity.x != 0 && controller.IsGrounded && !GameController.Instance.IsPaused);
        UpdateSound(extinguishingSound, extinguisher.IsTurnedOn && !GameController.Instance.IsPaused);
    }

    private void UpdateSound(Sound sound, bool condition)
    {
        if(condition)
        {
            if(!sound.Source.isPlaying) sound.Play();
        }
        else sound.Source.Stop(); 
    }
}