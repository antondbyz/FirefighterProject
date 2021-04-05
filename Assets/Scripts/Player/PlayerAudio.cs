using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    [SerializeField] private AudioClip[] deathClips = null;
    [SerializeField] private AudioClip jumpClip = null;
    [SerializeField] private AudioClip keyCollectedClip = null;
    [SerializeField] private AudioClip victimSavedClip = null;
    [SerializeField] private AudioClip levelCompletedClip = null;
    [SerializeField] private AudioClip levelFailedClip = null;
    [SerializeField] private AudioSource runningSource = null;
    [SerializeField] private AudioSource extinguishingSource = null;

    private PlayerController controller;
    private Player player;
    private Extinguisher extinguisher;

    private void Awake() 
    {
        controller = GetComponent<PlayerController>();   
        player = GetComponent<Player>();
        extinguisher = GetComponentInChildren<Extinguisher>(); 
        GameController.Instance.LevelCompleted.AddListener(PlayLevelCompleted);
        GameController.Instance.LevelFailed.AddListener(PlayLevelFailed);
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
        runningSource.Stop();    
        extinguishingSource.Stop();
    }

    private void Update() 
    {
        UpdateSource(runningSource, controller.NewVelocity.x != 0 && controller.IsGrounded && !GameController.Instance.IsPaused);
        UpdateSource(extinguishingSource, extinguisher.IsTurnedOn && !GameController.Instance.IsPaused);
    }

    private void PlayRandomDeath() => AudioManager.Instance.PlayClip(deathClips[Random.Range(0, deathClips.Length)]);

    private void PlayKeyCollected() => AudioManager.Instance.PlayClip(keyCollectedClip);

    private void PlayVictimSaved() => AudioManager.Instance.PlayClip(victimSavedClip);

    private void PlayJump() => AudioManager.Instance.PlayClip(jumpClip);

    private void PlayLevelCompleted() => AudioManager.Instance.PlayClip(levelCompletedClip);

    private void PlayLevelFailed() => AudioManager.Instance.PlayClip(levelFailedClip);

    private void UpdateSource(AudioSource source, bool condition)
    {
        if(condition)
        {
            if(!source.isPlaying) source.Play();
        }
        else source.Stop(); 
    }
}