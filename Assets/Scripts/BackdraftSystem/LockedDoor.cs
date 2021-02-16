using UnityEngine;

public class LockedDoor : BackdraftObstacle
{
    public event System.Action Unlocked;

    [SerializeField] private Sprite opened = null;
    [SerializeField] private GameObject unlockZone = null;
    [SerializeField] private GameObject key = null;

    private Collider2D myCollider;
    private SpriteRenderer myRenderer;
    private Transform myTransform;

    private void Awake() 
    {
        myCollider = GetComponent<Collider2D>();
        myRenderer = GetComponent<SpriteRenderer>();
        myTransform = transform;    
    }

    public void TryUnlock(float unlockerXPos)
    {
        if(key == null)
        {
            myCollider.enabled = false;
            myRenderer.flipX = unlockerXPos > myTransform.position.x;
            myRenderer.sprite = opened;
            unlockZone.SetActive(false);
            InvokeObstacleDisappeared();
            Unlocked?.Invoke();
        }
    }
}