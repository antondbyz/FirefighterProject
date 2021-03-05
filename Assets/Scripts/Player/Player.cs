using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event System.Action Died;
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
            earnedMoneyText.text = earnedMoney.ToString();
        }
    }
    public Checkpoint CurrentCheckpoint
    {
        get => currentCheckpoint;
        set
        {
            if(currentCheckpoint != value)
            {
                currentCheckpoint.IsActive = false;
                currentCheckpoint = value;
                currentCheckpoint.IsActive = true;
            }
        }
    }

    [SerializeField] private Checkpoint currentCheckpoint = null;
    [SerializeField] private TMP_Text lifesLeftText = null;
    [SerializeField] private TMP_Text earnedMoneyText = null;

    private Transform myTransform;
    private int lifesLeft;
    private int earnedMoney;

    public void Die() 
    {
        if(gameObject.activeSelf)
        {
            LifesLeft--;
            Died?.Invoke();
        }
    }

    public void MoveToCurrentCheckpoint() => myTransform.position = CurrentCheckpoint.transform.position;

    private void Awake() 
    {
        myTransform = transform;  
        LifesLeft = GameManager.CurrentPlayerSkin.LifesAmount;
        EarnedMoney = GameManager.PlayerBalance;
        currentCheckpoint.IsActive = true;
        MoveToCurrentCheckpoint();   
    }

    private void OnEnable() => Fire.Extinguished += FireExtinguished;

    private void OnDisable() => Fire.Extinguished -= FireExtinguished;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Victim")) 
        {
            Destroy(other.gameObject);
            EarnedMoney += GameManager.VICTIM_SAVED_REWARD;
        } 
        else if(other.CompareTag("Finish")) GameController.Instance.CompleteLevel();
        else if(other.CompareTag("DeathZone")) Die();
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.CompareTag("DeathZone")) Die();    
    }

    private void FireExtinguished() => EarnedMoney += GameManager.FIRE_EXTINGUISHED_REWARD;
}