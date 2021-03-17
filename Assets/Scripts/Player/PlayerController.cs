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
    [HideInInspector] public bool IsGrounded;
    [HideInInspector] public Vector2 NewVelocity;
    [HideInInspector] public bool IsHoldingLedge;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private PhysicsMaterial2D noFriction = null;
    [SerializeField] private PhysicsMaterial2D fullFriction = null;
    [SerializeField] private LayerMask whatIsGround = new LayerMask();
    [SerializeField] private LayerMask whatIsLedge = new LayerMask();
    [SerializeField] private float ledgeCheckOffsetY = 0;
    [SerializeField] private float ledgeGrabDistance = 0.1f;
    [SerializeField] private float hangTime = 0.1f;
    [SerializeField] private float jumpTime = 0.2f;
    
    private Transform myTransform;
    private Rigidbody2D rb;
    private BoxCollider2D bc;
    private PlayerAim aim;
    private bool flipX;
    private float defaultGravity;
    private float jumpTimer;
    private float hangTimer;
    private Vector2 LedgeCheckPos => (Vector2)bc.bounds.center + (Vector2.up * ledgeCheckOffsetY);

    private void Awake()
    {
        myTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        bc = GetComponent<BoxCollider2D>();
        aim = GetComponent<PlayerAim>();
        defaultGravity = rb.gravityScale;
    } 

    private void OnEnable() => FlipX = false;

    private void OnDisable() 
    { 
        NewVelocity.Set(0, 0);
        rb.velocity = NewVelocity;
    }

    private void Update() 
    {
        if(jumpTimer > 0) jumpTimer -= Time.deltaTime;
        NewVelocity.y = rb.velocity.y;
        CheckInput();
        IsGrounded = Physics2D.BoxCast(bc.bounds.center, bc.size, 0, Vector2.down, groundCheckDistance, whatIsGround);
        rb.sharedMaterial = NewVelocity.x != 0 || !IsGrounded ? noFriction : fullFriction;
        if(IsGrounded)
        {
            if(jumpTimer <= 0) CheckSlope();
            hangTimer = hangTime;
        }
        else
        {
            CheckLedgeGrab();
            if(hangTimer > 0) hangTimer -= Time.deltaTime;
        }
    }

    private void FixedUpdate() 
    {
        rb.velocity = NewVelocity;
    }

    private void CheckSlope()
    {
        Vector2 rayOrigin = new Vector2(bc.bounds.center.x, bc.bounds.center.y - bc.size.y / 2);
        RaycastHit2D hitDown = Physics2D.Raycast(rayOrigin, Vector2.down, 1, whatIsGround);
        if(Vector2.Angle(hitDown.normal, Vector2.up) > 0)
        {
            rayOrigin.y += 0.2f;
            RaycastHit2D hitFront = Physics2D.Raycast(rayOrigin, myTransform.right, 1, whatIsGround);
            float groundNormalDot = Vector2.Dot(hitDown.normal, myTransform.right);
            if(groundNormalDot < 0 && !hitFront) NewVelocity.y = 0;
            else NewVelocity = -Vector2.Perpendicular(hitDown.normal) * NewVelocity.x;
        }
    }

    private void CheckLedgeGrab()
    {
        float rayDistance = bc.size.x / 2 + ledgeGrabDistance;
        bool obstacleCheck = Physics2D.Raycast(bc.bounds.center, myTransform.right, rayDistance, whatIsLedge);
        bool ledgeCheck = !Physics2D.Raycast(LedgeCheckPos, myTransform.right, rayDistance, whatIsLedge);
        IsHoldingLedge = jumpTimer <= 0 && obstacleCheck && (ledgeCheck || IsHoldingLedge);
        if(IsHoldingLedge) 
        {
            hangTimer = hangTime;
            NewVelocity.Set(0, 0);
            rb.velocity = NewVelocity;
            rb.gravityScale = 0;
            if(ledgeCheck)
            {
                Vector2 verticalRayOrigin = LedgeCheckPos + (Vector2)(myTransform.right * rayDistance);  
                RaycastHit2D verticalHit = Physics2D.Raycast(verticalRayOrigin, Vector2.down, 0.5f, whatIsLedge);
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
            NewVelocity.x = InputManager.Horizontal * speed;
            if(InputManager.Horizontal != 0) FlipX = InputManager.Horizontal < 0;
            if(InputManager.JumpPressed && hangTimer > 0) 
            {
                jumpTimer = jumpTime;
                NewVelocity.y = jumpForce;
                rb.velocity = NewVelocity;
            }
        }
    }

    private void OnDrawGizmosSelected() 
    {
        if(bc == null) bc = GetComponent<BoxCollider2D>();
        Gizmos.color = Color.red;
        Gizmos.DrawSphere(LedgeCheckPos, 0.1f);    
    }
}