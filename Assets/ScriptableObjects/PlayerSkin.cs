using UnityEngine;

[CreateAssetMenu(menuName = "My assets/New player skin")]
public class PlayerSkin : ScriptableObject 
{
    public Sprite SkinPreview;
    public int Cost;
    [Header("Player body parts")]
    public Sprite Head;   
    public Sprite Body;   
    public Sprite RightArm;   
    public Sprite LeftArm;   
    public Sprite RightLeg;   
    public Sprite LeftLeg;   
    [Header("Extinguisher parts")]
    public Sprite ExtinguisherBalloon;
    public Sprite ExtinguisherHoseHidden;
    public Sprite ExtinguisherHoseDrawn;
}