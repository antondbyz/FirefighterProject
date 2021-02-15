using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private Sprite openedDoor = null;

    private Collider2D myCollider;
    private SpriteRenderer myRenderer;

    private void Awake() 
    {
        myCollider = GetComponent<Collider2D>();
        myRenderer = GetComponent<SpriteRenderer>();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Player"))
        {
            myRenderer.sprite = openedDoor;
            myCollider.enabled = false;
        }
    }
}