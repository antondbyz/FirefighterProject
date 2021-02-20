using UnityEngine;

public class GameManager : MonoBehaviour
{
    public const int FIRE_EXTINGUISHING_REWARD = 10;
    public const int VICTIM_SAVING_REWARD = 100;

    public static int PlayerBalance = 0;
    public static PlayerSkin[] PlayerSkins;
    public static PlayerSkin CurrentPlayerSkin;

    private void Awake() 
    {
        if(PlayerSkins == null)
        {
            PlayerSkins = Resources.LoadAll<PlayerSkin>("PlayerSkins");    
            CurrentPlayerSkin = PlayerSkins[0];
        }
    }   
}