using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public event System.Action OnMovingStarted;
    public event System.Action OnDirectionChanged;
    public Vector2 CurrentDirection { get; private set; }
    [Range(0, 10)] [SerializeField] private float speed = 5;
    private Health health;
    private Transform myTransform;
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private Vector2 movement;

    private void Awake()
    {
        health = GetComponent<Health>();
        myTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        GetComponentInChildren<ExtinguishingSubstance>().OnEmittingStarted += StopMovingX;
    }  

    private void FixedUpdate() 
    {
        if(movement.x != 0)
        {
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }    
    }

    public void LookAtTouchPosition()
    {
        CurrentDirection = TouchHandler.Instance.WorldTouchPosition - (Vector2)myTransform.position;
        rend.flipX = CurrentDirection.x < 0;
        if(movement.x != 0) movement.x = CurrentDirection.x >= 0 ? speed : -speed;
        OnDirectionChanged?.Invoke();
    }

    public void StartMovingX()
    {
        movement.x = CurrentDirection.x >= 0 ? speed : -speed;
        OnMovingStarted?.Invoke();
    }

    public void StopMovingX()
    {
        movement.x = 0;
        rb.velocity = movement;
    }
}