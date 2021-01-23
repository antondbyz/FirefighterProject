using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const int START_LIFES = 3;

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
    public int SavedVictims
    {
        get => savedVictims;
        private set
        {
            savedVictims = value;
            savedVictimsText.text = savedVictims.ToString();
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
    [SerializeField] private TMP_Text savedVictimsText = null;
    [SerializeField] private TMP_Text earnedMoneyText = null;

    private Transform myTransform;
    private Vector2 currentCheckpoint;
    private int lifesLeft;
    private int savedVictims;
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
        LifesLeft = START_LIFES;
        SavedVictims = 0;
        EarnedMoney = 0;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Victim")) 
        {
            Destroy(other.gameObject);
            SavedVictims++;
        } 
        else if(other.CompareTag("Finish")) GameController.Instance.CompleteLevel();
        else if(other.CompareTag("DeathZone")) Die();
    }

    private void OnParticleCollision(GameObject other) 
    {
        if(other.CompareTag("DeathZone")) Die();    
    }
}