using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event System.Action Died;
    public event System.Action KeyCollected;
    public event System.Action VictimSaved;
    public Checkpoint CurrentCheckpoint
    {
        get => currentCheckpoint;
        set
        {
            if(currentCheckpoint != value)
            {
                currentCheckpoint.IsActive = false;
                currentCheckpoint = value;
                currentCheckpoint.Activate();
            }
        }
    }
    public int LifesLeft
    {
        get => lifesLeft;
        private set
        {
            lifesLeft = value;
            lifesLeftText.text = lifesLeft.ToString();
        }
    }
    public int EarnedMoney
    {
        get => earnedMoney;
        private set
        {
            earnedMoney = value;
            earnedMoneyText.text = (GameManager.PlayerBalance + earnedMoney).ToString();
        }
    }
    public int VictimsSaved
    {
        get => victimsSaved;
        private set
        {
            victimsSaved = value;
            victimsSavedText.text = $"{victimsSaved}/{VictimsAmount}";
        }
    }
    public int VictimsAmount { get; private set; }
    public int FiresExtinguished { get; private set; }
    public int FiresAmount { get; private set; }

    [SerializeField] private bool moveToCurrentCheckpointOnAwake = true;
    [SerializeField] private ParticleSystem bloodEffect = null;
    [SerializeField] private TMP_Text lifesLeftText = null;
    [SerializeField] private TMP_Text earnedMoneyText = null;
    [SerializeField] private TMP_Text victimsSavedText = null;

    private Transform myTransform;
    private int lifesLeft;
    private int earnedMoney;
    private int victimsSaved;
    private Checkpoint currentCheckpoint;

    public void Die() 
    {
        if(gameObject.activeSelf)
        {
            bloodEffect.transform.position = myTransform.position;
            bloodEffect.Play();
            LifesLeft--;
            Died?.Invoke();
        }
    }

    public void MoveToCurrentCheckpoint() => myTransform.position = CurrentCheckpoint.transform.position;

    private void Awake() 
    {
        myTransform = transform;  
        LifesLeft = PlayerSkinInitializer.CurrentPlayerSkin.LifesAmount;
        EarnedMoney = 0;
        VictimsAmount = GameObject.FindWithTag("VictimsContainer").transform.childCount;
        VictimsSaved = 0;
        FiresAmount = GameObject.FindWithTag("FiresContainer").transform.childCount;
        currentCheckpoint = GameObject.FindWithTag("CheckpointsContainer").transform.GetChild(0).GetComponent<Checkpoint>();
        currentCheckpoint.IsActive = true;
        if(moveToCurrentCheckpointOnAwake) MoveToCurrentCheckpoint();   
    }

    private void OnEnable() => Fire.Extinguished += FireExtinguished;

    private void OnDisable() => Fire.Extinguished -= FireExtinguished;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Victim")) 
        {
            Destroy(other.gameObject);
            EarnedMoney += GameManager.VICTIM_SAVED_REWARD;
            VictimsSaved++;
            VictimSaved?.Invoke();
        } 
        else if(other.CompareTag("Finish")) GameController.Instance.CompleteLevel();
        else if(other.CompareTag("DeathZone")) Die();
        else if(other.CompareTag("Key"))
        {
            Destroy(other.gameObject);  
            KeyCollected?.Invoke();
        }
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.CompareTag("DeathZone")) Die();    
    }

    private void FireExtinguished() 
    { 
        EarnedMoney += GameManager.FIRE_EXTINGUISHED_REWARD;
        FiresExtinguished++;
    }
}