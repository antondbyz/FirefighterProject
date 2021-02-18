using UnityEngine;

public class PlayerSkinsManager : MonoBehaviour
{
    [SerializeField] private int currentSkinIndex = 0;
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

    private PlayerSkin[] skins;

    public void Initialize()
    {
        skins = Resources.LoadAll<PlayerSkin>("PlayerSkins");
        currentSkinIndex = Mathf.Clamp(currentSkinIndex, 0, skins.Length - 1);
        UpdateSkin(); 
    }

    private void Awake() => Initialize();

    private void UpdateSkin()
    {
        head.sprite = skins[currentSkinIndex].Head;
        body.sprite = skins[currentSkinIndex].Body;
        rightArm.sprite = skins[currentSkinIndex].RightArm;
        leftArm.sprite = skins[currentSkinIndex].LeftArm;
        rightLeg.sprite = skins[currentSkinIndex].RightLeg;
        leftLeg.sprite = skins[currentSkinIndex].LeftLeg;
        extinguisherBalloon.sprite = skins[currentSkinIndex].ExtinguisherBalloon;
        extinguisherHoseHidden.sprite = skins[currentSkinIndex].ExtinguisherHoseHidden;
        extinguisherHoseDrawn.sprite =skins[currentSkinIndex].ExtinguisherHoseDrawn;
    }
}