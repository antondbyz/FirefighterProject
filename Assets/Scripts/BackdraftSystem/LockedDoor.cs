using UnityEngine;

public class LockedDoor : BackdraftObstacle
{
    [SerializeField] private Sprite opened = null;
    [SerializeField] private GameObject unlockZone = null;
    [SerializeField] private AudioSource audioSource = null;
    [SerializeField] private AudioClip unlockedClip = null;
    [SerializeField] private AudioClip lockedClip = null;
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

    public bool TryUnlock(float unlockerXPos)
    {
        if(key == null)
        {
            myCollider.enabled = false;
            myRenderer.flipX = unlockerXPos > myTransform.position.x;
            myRenderer.sprite = opened;
            unlockZone.SetActive(false);
            audioSource.clip = unlockedClip;
            audioSource.Play();
            Disappeared?.Invoke();
            return true;
        }
        audioSource.clip = lockedClip;
        audioSource.Play();
        return false;
    }
}