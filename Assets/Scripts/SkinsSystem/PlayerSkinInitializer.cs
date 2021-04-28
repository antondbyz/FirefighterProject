using UnityEngine;

public class PlayerSkinInitializer : SkinInitializer
{
    public static PlayerSkin CurrentPlayerSkin;

    [SerializeField] private bool loadCurrentSkinOnAwake = true;
    [SerializeField] private SpriteRenderer extinguisherBalloon = null;
    [SerializeField] private SpriteRenderer extinguisherHoseHidden = null;
    [SerializeField] private SpriteRenderer extinguisherHoseDrawn = null;

    public override void UpdateSkin()
    {
        base.UpdateSkin();
        extinguisherBalloon.sprite = CurrentPlayerSkin.ExtinguisherBalloon;
        extinguisherHoseHidden.sprite = CurrentPlayerSkin.ExtinguisherHoseHidden;
        extinguisherHoseDrawn.sprite = CurrentPlayerSkin.ExtinguisherHoseDrawn;
    }

    private void Awake() 
    { 
        if(loadCurrentSkinOnAwake) currentSkin = GameManager.PlayerSkins[ShopManager.UsingItemIndex];
        CurrentPlayerSkin = (PlayerSkin)currentSkin;
        UpdateSkin();
    }
}