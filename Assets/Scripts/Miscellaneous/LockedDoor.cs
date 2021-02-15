using UnityEngine;

public class LockedDoor : MonoBehaviour
{
    [SerializeField] private Sprite openedDoor = null;
    [SerializeField] private GameObject key = null;

    private Collider2D doorCollider;
    private SpriteRenderer doorRenderer;

    private void Awake() 
    {
        Transform parent = transform.parent;
        doorCollider = parent.GetComponent<Collider2D>();
        doorRenderer = parent.GetComponent<SpriteRenderer>();    
    }

    public void TryUnlock()
    {
        if(key == null)
        {
            doorCollider.enabled = false;
            doorRenderer.sprite = openedDoor;
        }
    }
}
