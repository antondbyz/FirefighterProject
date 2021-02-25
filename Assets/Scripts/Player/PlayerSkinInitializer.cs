using UnityEngine;

public class PlayerSkinInitializer : MonoBehaviour
{
    [Header("Player body parts")]
    [SerializeField] private SpriteRenderer head = null;
    [SerializeField] private SpriteRenderer body = null;
    [SerializeField] private SpriteRenderer rightArm = null;
    [SerializeField] private SpriteRenderer leftArm = null;
    [SerializeField] private SpriteRenderer rightLeg = null;
    [SerializeField] private SpriteRenderer leftLeg = null;
    [Header("Extinguisher parts")]
    [SerializeField] private SpriteRenderer extinguisherBalloon = null;
    [SerializeField] private SpriteRenderer extinguisherHoseHidden = null;
    [SerializeField] private SpriteRenderer extinguisherHoseDrawn = null;

    public void UpdateSkin()
    {
        head.sprite = GameManager.CurrentPlayerSkin.Head;
        body.sprite = GameManager.CurrentPlayerSkin.Body;
        rightArm.sprite = GameManager.CurrentPlayerSkin.RightArm;
        leftArm.sprite = GameManager.CurrentPlayerSkin.LeftArm;
        rightLeg.sprite = GameManager.CurrentPlayerSkin.RightLeg;
        leftLeg.sprite = GameManager.CurrentPlayerSkin.LeftLeg;
        extinguisherBalloon.sprite = GameManager.CurrentPlayerSkin.ExtinguisherBalloon;
        extinguisherHoseHidden.sprite = GameManager.CurrentPlayerSkin.ExtinguisherHoseHidden;
        extinguisherHoseDrawn.sprite =GameManager.CurrentPlayerSkin.ExtinguisherHoseDrawn;
    }

    private void Awake() => UpdateSkin();
}