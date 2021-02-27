using UnityEngine;

public class SkinInitializer : MonoBehaviour 
{
    [SerializeField] protected Skin currentSkin = null;
    [SerializeField] protected SpriteRenderer head = null;
    [SerializeField] protected SpriteRenderer body = null;
    [SerializeField] protected SpriteRenderer rightArm = null;
    [SerializeField] protected SpriteRenderer leftArm = null;
    [SerializeField] protected SpriteRenderer rightLeg = null;
    [SerializeField] protected SpriteRenderer leftLeg = null;

    public virtual void UpdateSkin()
    {
        head.sprite = currentSkin.Head;
        body.sprite = currentSkin.Body;
        rightArm.sprite = currentSkin.RightArm;
        leftArm.sprite = currentSkin.LeftArm;
        rightLeg.sprite = currentSkin.RightLeg;
        leftLeg.sprite = currentSkin.LeftLeg;
    }  
}