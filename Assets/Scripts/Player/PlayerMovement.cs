using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 250;
    
    private Rigidbody2D rb;
    private SpriteRenderer rend;
    private BoxCollider2D coll;
    private Vector2 movement;

    public void SetMovementX(float value)
    {
        movement.x = value * speed;
        if(value < 0) rend.flipX = true;
        else if(value > 0) rend.flipX = false;
    }

    public void Jump()
    {
        if(IsGrounded()) 
            rb.AddForce(Vector2.up * jumpForce);
    }

    public bool IsGrounded()
    {
        Vector2 size = new Vector2(coll.bounds.size.x, coll.bounds.size.y / 2);
        float distance = (coll.bounds.extents.y / 2) + 0.01f;
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, size, 0, Vector2.down, distance);
        return hit;
    }

#if UNITY_EDITOR
    private void Update() 
    {
        SetMovementX(Input.GetAxisRaw("Horizontal"));
        if(Input.GetKeyDown(KeyCode.UpArrow)) Jump();
    }
#endif

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
        coll = GetComponent<BoxCollider2D>();
    } 

    private void FixedUpdate() 
    {
        movement.y = rb.velocity.y;  
        rb.velocity = movement;
    }
}