using UnityEngine;

public class LockedDoor : BackdraftObstacle
{
    public event System.Action Unlocked;

    [SerializeField] private Sprite opened = null;
    [SerializeField] private GameObject unlockZone = null;
    [SerializeField] private GameObject key = null;

    private Collider2D myCollider;
    private SpriteRenderer myRenderer;

    private void Awake() 
    {
        myCollider = GetComponent<Collider2D>();
        myRenderer = GetComponent<SpriteRenderer>();    
    }

    public void TryUnlock()
    {
        if(key == null)
        {
            myCollider.enabled = false;
            myRenderer.sprite = opened;
            unlockZone.SetActive(false);
            InvokeObstacleDisappeared();
            Unlocked?.Invoke();
        }
    }
}