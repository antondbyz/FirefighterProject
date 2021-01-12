using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    public const int MAX_LIFES = 3;

    public Vector2 Direction => myTransform.right;
    public Vector2 ColliderCenter => bc.bounds.center;
    public event System.Action Died;
    public int LifesLeft
    {
        get => lifesLeft;
        private set
        {
            value = Mathf.Clamp(value, 0, MAX_LIFES);
            lifesLeft = value;
            lifesLeftText.text = lifesLeft.ToString();
        }
    }
    public int VictimsSaved
    {
        get => victimsSaved;
        private set
        {
            victimsSaved = value;
            victimsSavedText.text = $"{victimsSaved}/{victimsTotal}";
        }
    }

    [SerializeField] private TMP_Text lifesLeftText = null;
    [SerializeField] private TMP_Text victimsSavedText = null;
    [SerializeField] private Transform victims = null;

    private Transform myTransform;
    private BoxCollider2D bc;
    private Vector2 currentCheckpoint;
    private int lifesLeft;
    private int victimsSaved;
    private int victimsTotal;

    public void MoveToCurrentCheckpoint() => myTransform.position = currentCheckpoint;

    private void Awake() 
    {
        myTransform = transform;  
        bc = GetComponent<BoxCollider2D>();
        currentCheckpoint = myTransform.position;
        LifesLeft = MAX_LIFES;
        victimsTotal = victims.childCount;
        VictimsSaved = 0;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Victim")) 
        {
            Destroy(other.gameObject);
            VictimsSaved++;
        } 
        else if(other.CompareTag("Finish")) GameController.Instance.CompleteLevel();
        else if(gameObject.activeSelf && other.CompareTag("DeathZone")) 
        {
            LifesLeft--;
            Died?.Invoke();
        }  
    }
}