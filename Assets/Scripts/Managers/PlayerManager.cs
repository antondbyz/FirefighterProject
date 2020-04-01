using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static PlayerManager Instance;
    public PlayerLevel CurrentLevel => playerLevels[CurrentLevelIndex];
    public int CurrentLevelIndex { get; private set; }
    public bool CanUpgrade => !IsMaxLevel && CurrentBalance >= playerLevels[CurrentLevelIndex + 1].Price;
    public bool IsMaxLevel => CurrentLevelIndex == playerLevels.Length - 1;
    public int CurrentBalance { get; private set; }
    [SerializeField] private PlayerLevel[] playerLevels = null;

    private void Awake() 
    {
        if(Instance == null) Instance = this;
        else Destroy(gameObject);    
    }

    public void Upgrade()
    {
        if(CanUpgrade)
        {
            CurrentBalance -= playerLevels[CurrentLevelIndex + 1].Price;
            CurrentLevelIndex++;
        }
    }
}