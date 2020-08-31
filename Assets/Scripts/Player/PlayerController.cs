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
    private bool flipX;

    public bool IsGrounded 
    {
        get
        {
            float distance = cc.size.y / 2 - cc.size.x / 2 + groundCheckDistance;
            return Physics2D.CircleCast(cc.bounds.center, cc.size.x / 2, Vector2.down, distance);
        }
    }  
    public bool IsMoving => newVelocity.x != 0;
    public bool IsHoldingLedge { get; private set; }

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private LayerMask whatCanGrab = new LayerMask();
    [SerializeField] private PhysicsMaterial2D noFriction = null;
    [SerializeField] private PhysicsMaterial2D fullFriction = null;
    
    private Transform myTransform;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private PlayerInput input;
    private PlayerAim aim;
    private Vector2 newVelocity;
    private bool isJumping;
    private float defaultGravity;

    private void Awake()
    {
        myTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        input = GetComponent<PlayerInput>();
        aim = GetComponent<PlayerAim>();
        FlipX = false;
        defaultGravity = rb.gravityScale;
    } 

    private void OnDisable() 
    { 
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
            Vector2 rayOrigin = new Vector2(cc.bounds.center.x, cc.bounds.center.y - cc.size.y / 2);
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down);
            if(Vector2.Angle(hit.normal, Vector2.up) > 0)
            {
                float groundNormalDot = Vector2.Dot(hit.normal, Vector2.right);
                RaycastHit2D hitFront = Physics2D.Raycast(rayOrigin + Vector2.up * 0.2f, Vector2.right, 0.5f);
                RaycastHit2D hitBack = Physics2D.Raycast(rayOrigin + Vector2.up * 0.2f, Vector2.left, 0.5f);
                if((newVelocity.x > 0 && groundNormalDot < 0 && !hitFront) ||
                   (newVelocity.x < 0 && groundNormalDot > 0 && !hitBack))   
                {
                    newVelocity.y = 0;
                }
                else newVelocity = -Vector2.Perpendicular(hit.normal) * newVelocity.x;
            }
        }
    }

    private void CheckLedgeGrab()
    {
        Vector2 rayDirection = FlipX ? Vector2.left : Vector2.right;
        Vector2 upperRayOrigin = new Vector2(cc.bounds.center.x, cc.bounds.center.y + 0.5f);
        RaycastHit2D upperHit = Physics2D.Raycast(upperRayOrigin, rayDirection, cc.size.x / 2 * 1.3f, whatCanGrab);
        RaycastHit2D bottomHit = Physics2D.Raycast(cc.bounds.center, rayDirection, cc.size.x / 2 * 1.3f, whatCanGrab);
        IsHoldingLedge = !isJumping && bottomHit && (!upperHit || IsHoldingLedge);
        if(IsHoldingLedge) 
        {
            newVelocity.Set(0, 0);
            rb.velocity = newVelocity;
            rb.gravityScale = 0;
            if(!upperHit)
            {
                Vector2 verticalRayOrigin = new Vector2(upperRayOrigin.x + rayDirection.x * cc.size.x / 2 * 1.5f, upperRayOrigin.y);                
                RaycastHit2D verticalHit = Physics2D.Raycast(verticalRayOrigin, Vector2.down, 0.5f, whatCanGrab);
                float offset = verticalRayOrigin.y - verticalHit.point.y;
                rb.position = new Vector2(rb.position.x, rb.position.y - offset);
            }
        }
        else rb.gravityScale = defaultGravity;
    }

    private void CheckInput()
    {
        if(!aim.IsAiming)
        {
            newVelocity.x = input.Horizontal * speed;
            if(input.Horizontal != 0) FlipX = input.Horizontal < 0;
            if(input.JumpPressed && (IsGrounded || IsHoldingLedge))
            {
                newVelocity.y = jumpForce;
                rb.velocity = newVelocity;
                isJumping = true;
            }
        }
    }
}