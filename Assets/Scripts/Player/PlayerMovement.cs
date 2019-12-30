using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] [Range(0, 10)] private float movementSpeed = 1;
    [SerializeField] private FireExtinguisher fireExtinguisher = null;

    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private Vector2 movement;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }   

    private void Update()
    {
        if(!fireExtinguisher.IsExtinguishing && Mathf.Abs(movement.x) > 0)
        {
            movement.y = rb.velocity.y;
            rb.velocity = movement;
        }
    }

    public void MoveX(float xSide)
    {
        movement.x = xSide * movementSpeed;
        if(xSide > 0) FlipX(false);
        else if(xSide < 0) FlipX(true);
    }

    public void StopX()
    {
        movement.x = 0;
        movement.y = rb.velocity.y;
        rb.velocity = movement;
    }

    public void FlipX(bool flipX)
    {
        if(rend.flipX != flipX) rend.flipX = flipX;
        if(fireExtinguisher.FlipX != flipX) fireExtinguisher.FlipX = flipX;
    }
}