using UnityEngine;

[CreateAssetMenu(menuName="New Player Level")]
public class PlayerLevel : ScriptableObject
{
    public GameObject Prefab => prefab;
    public Sprite Preview => preview;
    public int Price => price;
    
    [SerializeField] private GameObject prefab = null;
    [SerializeField] private Sprite preview = null;
    [SerializeField] private int price = 0;
}