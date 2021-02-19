using UnityEngine;

public class GameManager : MonoBehaviour
{
    private void Awake() 
    {
        if(PlayerSkinsManager.Skins == null)
            PlayerSkinsManager.Skins = Resources.LoadAll<PlayerSkin>("PlayerSkins");    
    }   
}