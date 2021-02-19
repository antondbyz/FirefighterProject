using UnityEngine;

public class PlayerSkinsManager : MonoBehaviour
{
    public static PlayerSkin[] Skins;
    public static int CurrentSkinIndex = 0;

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

    private void Awake() 
    {
        head.sprite = Skins[CurrentSkinIndex].Head;
        body.sprite = Skins[CurrentSkinIndex].Body;
        rightArm.sprite = Skins[CurrentSkinIndex].RightArm;
        leftArm.sprite = Skins[CurrentSkinIndex].LeftArm;
        rightLeg.sprite = Skins[CurrentSkinIndex].RightLeg;
        leftLeg.sprite = Skins[CurrentSkinIndex].LeftLeg;
        extinguisherBalloon.sprite = Skins[CurrentSkinIndex].ExtinguisherBalloon;
        extinguisherHoseHidden.sprite = Skins[CurrentSkinIndex].ExtinguisherHoseHidden;
        extinguisherHoseDrawn.sprite =Skins[CurrentSkinIndex].ExtinguisherHoseDrawn;
    }
}