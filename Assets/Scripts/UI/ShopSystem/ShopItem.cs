using UnityEngine;
using UnityEngine.UI;

public class ShopItem : MonoBehaviour
{
    public PlayerSkin Skin;

    [SerializeField] private Image previewImage;

    public void Initialize(PlayerSkin skin) 
    { 
        Skin = skin;
        previewImage.sprite = Skin.SkinPreview;
    }
}