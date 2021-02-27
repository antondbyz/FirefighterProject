using UnityEngine;

public class PlayerSkinInitializer : SkinInitializer
{
    [SerializeField] private SpriteRenderer extinguisherBalloon = null;
    [SerializeField] private SpriteRenderer extinguisherHoseHidden = null;
    [SerializeField] private SpriteRenderer extinguisherHoseDrawn = null;

    public override void UpdateSkin()
    {
        base.UpdateSkin();
        PlayerSkin skin = (PlayerSkin)currentSkin;
        extinguisherBalloon.sprite = skin.ExtinguisherBalloon;
        extinguisherHoseHidden.sprite = skin.ExtinguisherHoseHidden;
        extinguisherHoseDrawn.sprite = skin.ExtinguisherHoseDrawn;
    }

    private void Awake() 
    { 
        currentSkin = GameManager.CurrentPlayerSkin;
        UpdateSkin();
    }
}