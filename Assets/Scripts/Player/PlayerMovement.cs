using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public bool FlipX 
    {
        get => flipX;
        set 
        {
            flipX = value;
            myTransform.rotation = value ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        }
    }
    private bool flipX;

    public bool IsMoving => movement.x != 0;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 250;
    
    private Transform myTransform;
    private Rigidbody2D rb;
    private BoxCollider2D coll;
    private Animator animator;
    private PlayerRotation rotation;
    private Vector2 movement;

    /// direction that is greater or equals 0 is right
    /// direction that is less than 0 is left
    public void StartMoving(int direction)
    {
        if(!rotation.IsAiming)
        {
            movement.x = direction >= 0 ? speed : -speed;
            FlipX = direction < 0;
            animator.SetBool("Running", true);
            rotation.ResetRotation();
        }
    }

    public void StopMoving()
    {
        movement.x = 0;
        animator.SetBool("Running", false);
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
        return Physics2D.BoxCast(coll.bounds.center, size, 0, Vector2.down, distance);
    }

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
        rotation = GetComponent<PlayerRotation>();
    }

    #if UNITY_EDITOR
    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.RightArrow)) StartMoving(1);
        else if(Input.GetKeyDown(KeyCode.LeftArrow)) StartMoving(-1);
        else if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.LeftArrow)) StopMoving();

        if(Input.GetKeyDown(KeyCode.UpArrow)) Jump();
    }
#endif 

    private void FixedUpdate() 
    {
        movement.y = rb.velocity.y;  
        rb.velocity = movement;
    }
}