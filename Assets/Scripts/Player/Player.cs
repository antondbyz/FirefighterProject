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

    [SerializeField] private TMP_Text lifesLeftText = null;
    [SerializeField] private TMP_Text earnedMoneyText = null;

    private Transform myTransform;
    private Vector2 currentCheckpoint;
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

    public void MoveToCurrentCheckpoint() => myTransform.position = currentCheckpoint;

    private void Awake() 
    {
        myTransform = transform;  
        currentCheckpoint = myTransform.position;
        LifesLeft = GameManager.CurrentPlayerSkin.LifesAmount;
        EarnedMoney = GameManager.PlayerBalance;
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

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("DeathZone")) Die();
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.CompareTag("DeathZone")) Die();    
    }

    private void FireExtinguished() => EarnedMoney += GameManager.FIRE_EXTINGUISHED_REWARD;
}