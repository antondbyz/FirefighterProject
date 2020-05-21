using UnityEngine;
using TMPro;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    public int EarnedMoney
    {
        get => earnedMoney;
        private set
        {
            earnedMoney = value;
            moneyText.text = $"{earnedMoney}$";
        }
    }
    [SerializeField] private TextMeshProUGUI moneyText = null;
    [SerializeField] private UnityEvent levelFailed = null;
    [SerializeField] private UnityEvent levelCompleted = null;
    private int earnedMoney;
    private Health health;

    private void Awake() 
    {
        PauseManager.IsPaused = false;
        health = GetComponent<Health>();
        EarnedMoney = 0;    
    }

    private void OnEnable() 
    {
        if(health != null)
            health.Died += FailLevel;
    }

    private void OnDisable() 
    {
        if(health != null)
            health.Died -= FailLevel;    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Finish finish = other.GetComponent<Finish>();
        if(finish != null)
        {
            EarnedMoney += finish.Reward;
            CompleteLevel();
        }        
    }

    public void FailLevel()
    {
        PauseManager.IsPaused = true;
        levelFailed.Invoke();
    }

    public void CompleteLevel()
    {
        PauseManager.IsPaused = true;
        levelCompleted.Invoke();
    }
}