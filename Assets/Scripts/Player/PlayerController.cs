using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public bool FlipX 
    {
        get => flipX;
        private set 
        {
            flipX = value;
            myTransform.rotation = value ? Quaternion.Euler(0, 180, 0) : Quaternion.Euler(0, 0, 0);
        }
    }

    public bool IsGrounded => Physics2D.BoxCast(bc.bounds.center, bc.size, 0, Vector2.down, groundCheckDistance);
    public bool IsMoving => newVelocity.x != 0;
    public bool IsHoldingLedge { get; private set; }

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private PhysicsMaterial2D noFriction = null;
    [SerializeField] private PhysicsMaterial2D fullFriction = null;
    
    private Transform myTransform;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private PlayerAim aim;
    private Vector2 newVelocity;
    private bool flipX;
    private bool isJumping;
    private float defaultGravity;

    public void Jump()
    {
        if((IsGrounded || IsHoldingLedge) && !aim.IsAiming)
        {
            newVelocity.y = jumpForce;
            rb.velocity = newVelocity;
            isJumping = true;
        }
    }

    private void Awake()
    {
        myTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        aim = GetComponent<PlayerAim>();
        defaultGravity = rb.gravityScale;
    } 

    private void OnEnable() 
    {
        InputManager.JumpPressed += Jump; 
        FlipX = false;
    }

    private void OnDisable() 
    { 
        InputManager.JumpPressed -= Jump;
        newVelocity.Set(0, 0);
        rb.velocity = newVelocity;
    }

    private void Update() 
    {
        newVelocity.y = rb.velocity.y;
        bool isGrounded = IsGrounded;
        CheckInput();
        rb.sharedMaterial = IsMoving || !isGrounded ? noFriction : fullFriction;
        if(isGrounded) CheckSlope();
        else CheckLedgeGrab();
        if(isGrounded || rb.velocity.y < 0) isJumping = false;
    }

    private void FixedUpdate() 
    {
        rb.velocity = newVelocity;
    }

    private void CheckSlope()
    {
        if(!isJumping)
        {
            Vector2 rayOrigin = new Vector2(bc.bounds.center.x, bc.bounds.center.y - bc.size.y / 2);
            RaycastHit2D hitDown = Physics2D.Raycast(rayOrigin, Vector2.down);
            if(Vector2.Angle(hitDown.normal, Vector2.up) > 0)
            {
                rayOrigin.y += 0.2f;
                RaycastHit2D hitFront = Physics2D.Raycast(rayOrigin, myTransform.right, 1);
                float groundNormalDot = Vector2.Dot(hitDown.normal, myTransform.right);
                if(newVelocity.x != 0 && groundNormalDot < 0 && !hitFront) newVelocity.y = 0;
                else newVelocity = -Vector2.Perpendicular(hitDown.normal) * newVelocity.x;
            }
        }
    }

    private void CheckLedgeGrab()
    {
        float rayDistance = bc.size.x / 2 * 1.3f;
        Vector2 upperRayOrigin = new Vector2(bc.bounds.center.x, bc.bounds.center.y + 0.5f);
        RaycastHit2D upperHit = Physics2D.Raycast(upperRayOrigin, myTransform.right, rayDistance);
        RaycastHit2D bottomHit = Physics2D.Raycast(bc.bounds.center, myTransform.right, rayDistance);
        IsHoldingLedge = !isJumping && bottomHit && (!upperHit || IsHoldingLedge);
        if(IsHoldingLedge) 
        {
            newVelocity.Set(0, 0);
            rb.velocity = newVelocity;
            rb.gravityScale = 0;
            if(!upperHit)
            {
                Vector2 verticalRayOrigin = new Vector2(upperRayOrigin.x + myTransform.right.x * rayDistance, upperRayOrigin.y);                
                RaycastHit2D verticalHit = Physics2D.Raycast(verticalRayOrigin, Vector2.down, 0.5f);
                float offset = verticalHit.point.y - verticalRayOrigin.y - 0.05f;
                rb.position = new Vector2(rb.position.x, rb.position.y + offset);
            }
        }
        else rb.gravityScale = defaultGravity;
    }

    private void CheckInput()
    {
        if(!aim.IsAiming)
        {
            newVelocity.x = InputManager.Horizontal * speed;
            if(InputManager.Horizontal != 0) FlipX = InputManager.Horizontal < 0;
        }
    }
}