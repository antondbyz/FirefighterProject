using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event System.Action DirectionChanged;
    public bool IsMoving { get; set; }
    public Vector2 CurrentDirection { get; private set; }
    
    [Range(0, 10)] [SerializeField] private float speed = 5;
    private Transform myTransform;
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private Vector2 movement;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    } 

    private void Start() 
    {
        ScreenTouchesHandler.Instance.ScreenTouched += UpdateDirection;    
    }

    private void FixedUpdate() 
    {
        if(IsMoving) movement.x = CurrentDirection.x >= 0 ? speed : -speed; 
        else movement.x = 0;
        movement.y = rb.velocity.y;  
        rb.velocity = movement;
    }

    public void UpdateDirection()
    {
        CurrentDirection = ScreenTouchesHandler.Instance.WorldTouchPosition - (Vector2)myTransform.position;
        rend.flipX = CurrentDirection.x < 0;
        DirectionChanged?.Invoke();
    }

    private void OnDisable() 
    {
        ScreenTouchesHandler.Instance.ScreenTouched -= UpdateDirection;    
    }
}