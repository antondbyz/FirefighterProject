using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private bool keyboardMovement = false;
    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 250;
    
    private Transform myTransform;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Vector2 movement;

    public void SetMovementX(float value)
    {
        movement.x = value * speed;
        if(value < 0) myTransform.rotation = Quaternion.Euler(0, 180, 0);
        else if(value > 0) myTransform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void Jump()
    {
        if(IsGrounded()) 
            rb.AddForce(Vector2.up * jumpForce);
    }

    public bool IsGrounded()
    {
        Vector2 size = new Vector2(coll.bounds.size.x, coll.bounds.size.y / 2);
        float distance = (coll.bounds.extents.y / 2) + 0.05f;
        RaycastHit2D hit = Physics2D.BoxCast(coll.bounds.center, size, 0, Vector2.down, distance);
        return hit;
    }

#if UNITY_EDITOR
    private void Update() 
    {
        if(keyboardMovement)
        {
            SetMovementX(Input.GetAxisRaw("Horizontal"));
            if(Input.GetKeyDown(KeyCode.UpArrow)) Jump();
        }
    }
#endif

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
    } 

    private void FixedUpdate() 
    {
        movement.y = rb.velocity.y;  
        rb.velocity = movement;
    }
}