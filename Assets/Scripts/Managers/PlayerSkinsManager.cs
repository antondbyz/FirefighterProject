using UnityEngine;

public class PlayerSkinsManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer head = null;
    [SerializeField] private SpriteRenderer body = null;
    [SerializeField] private SpriteRenderer rightArm = null;
    [SerializeField] private SpriteRenderer leftArm = null;
    [SerializeField] private SpriteRenderer rightLeg = null;
    [SerializeField] private SpriteRenderer leftLeg = null;

    private PlayerSkin[] skins;
    private int currentSkinIndex = 0;

    private void Awake() 
    {
        skins = Resources.LoadAll<PlayerSkin>("PlayerSkins");
        UpdateSkin();    
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(currentSkinIndex >= skins.Length - 1) currentSkinIndex = 0;
            else currentSkinIndex++;
            UpdateSkin();
        }
    }

    private void UpdateSkin()
    {
        head.sprite = skins[currentSkinIndex].Head;
        body.sprite = skins[currentSkinIndex].Body;
        rightArm.sprite = skins[currentSkinIndex].RightArm;
        leftArm.sprite = skins[currentSkinIndex].LeftArm;
        rightLeg.sprite = skins[currentSkinIndex].RightLeg;
        leftLeg.sprite = skins[currentSkinIndex].LeftLeg;
    }
}