using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int FIRE_EXTINGUISHING_REWARD = 1000;
    public const int VICTIM_SAVING_REWARD = 100000;

    public static int PlayerBalance 
    {
        get => playerBalance;
        set
        {
            if(value < 0) value = 0;
            playerBalance = value;
        }
    }
    public static PlayerSkin[] PlayerSkins;
    public static PlayerSkin CurrentPlayerSkin;

    private static int playerBalance;

    private void Awake() 
    {
        if(PlayerSkins == null)
        {
            PlayerSkins = Resources.LoadAll<PlayerSkin>("PlayerSkins");    
            CurrentPlayerSkin = PlayerSkins[0];
        }
    }   
}