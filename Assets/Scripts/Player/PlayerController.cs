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

    public bool IsGrounded { get; private set; }  
    public bool IsMoving => newVelocity.x != 0 || newVelocity.y != 0;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private PhysicsMaterial2D noFriction = null;
    [SerializeField] private PhysicsMaterial2D fullFriction = null;
    
    private Transform myTransform;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private Animator animator;
    private PlayerAiming aiming;
    private Vector2 newVelocity;
    private Vector2 slopeNormalPerpendicular;
    private bool isOnSlope;
    private float slopeNormalDotProduct;
    private bool isJumping;

    public void SetVelocityX(float value)
    {
        if(!PlayerAiming.IsAiming)
        {
            newVelocity.x = value * speed;
            if(value > 0) FlipX = false;
            else if(value < 0) FlipX = true;
            animator.SetBool("Running", value != 0);
            if(value != 0) aiming.ResetRotation();
        }
    }

    public void Jump()
    {
        if(!PlayerAiming.IsAiming && IsGrounded)
        {
            newVelocity.Set(0, 0);
            rb.velocity = Vector2.up * jumpForce;
            isJumping = true;
        }
    }

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        aiming = GetComponent<PlayerAiming>();
        FlipX = false;
    } 

    private void FixedUpdate() 
    {
        GroundCheck();
        SlopeCheck();
        Move();
    }

    private void Move()
    {
        rb.sharedMaterial = IsMoving ? noFriction : fullFriction;
        if(IsGrounded && !isJumping)
        {
            if(isOnSlope)
            {
                Vector2 rayOrigin = new Vector2(cc.bounds.center.x, cc.bounds.center.y - cc.size.y / 2 + 0.2f);
                RaycastHit2D hitFront = Physics2D.Raycast(rayOrigin, Vector2.right, 0.5f);
                RaycastHit2D hitBack = Physics2D.Raycast(rayOrigin, Vector2.left, 0.5f);
                if((newVelocity.x > 0 && slopeNormalDotProduct < 0 && !hitFront) ||
                   (newVelocity.x < 0 && slopeNormalDotProduct > 0 && !hitBack))   
                {
                    newVelocity.y = 0;
                }
                else
                {
                    newVelocity = -slopeNormalPerpendicular * newVelocity.x;
                }
            }
            else newVelocity.y = rb.velocity.y;
        }
        else newVelocity.y = rb.velocity.y;
        rb.velocity = newVelocity;
        newVelocity.Set(0, 0);
        if(rb.velocity.y <= 0 || IsGrounded) isJumping = false;
    }

    private void GroundCheck()
    {
        float distance = cc.size.y / 2 - cc.size.x / 2 + groundCheckDistance;
        IsGrounded = Physics2D.CircleCast(cc.bounds.center, cc.size.x / 2, Vector2.down, distance);
    }

    private void SlopeCheck()
    {
        Vector2 rayOrigin = new Vector2(cc.bounds.center.x, cc.bounds.center.y - cc.size.y / 2);
        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, Vector2.down);
        isOnSlope = Vector2.Angle(hit.normal, Vector2.up) > 0;
        if(isOnSlope)
        {
            slopeNormalDotProduct = Vector2.Dot(hit.normal, Vector2.right);
            slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
        }
    }
}