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
    public bool IsMoving => input.Horizontal != 0;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 8;
    [SerializeField] private float groundCheckDistance = 0.05f;
    [SerializeField] private PhysicsMaterial2D noFriction = null;
    [SerializeField] private PhysicsMaterial2D fullFriction = null;
    
    private Transform myTransform;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private PlayerInput input;
    private PlayerAim aim;
    private Vector2 newVelocity;
    private bool pushing;
    private float pushingTimer;

    public void Push(Vector2 force)
    {
        pushing = true;
        rb.velocity = force;
    }

    private void Awake()
    {
        myTransform = transform;
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        input = GetComponent<PlayerInput>();
        aim = GetComponent<PlayerAim>();
        FlipX = false;
    } 

    private void Update() 
    {
        CheckInput();
        rb.sharedMaterial = IsMoving ? noFriction : fullFriction;
    }

    private void FixedUpdate() 
    {
        newVelocity.y = rb.velocity.y;
        if(IsGrounded) GroundMovement();
        if(pushing)
        {
            newVelocity = rb.velocity;
            pushingTimer += Time.fixedDeltaTime;
            if(IsGrounded && pushingTimer >= 0.1f)
            {
                pushing = false;
                pushingTimer = 0;
            }
        }
        rb.velocity = newVelocity;
    }

    private void GroundMovement()
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
            else
            {
                newVelocity = -Vector2.Perpendicular(hit.normal) * newVelocity.x;
            }
        }
        if(input.JumpPressed && !aim.IsAiming)
            newVelocity.y = jumpForce;
    }

    private void CheckInput()
    {
        if(!aim.IsAiming)
        {
            newVelocity.x = input.Horizontal * speed;
            if(input.Horizontal != 0)
            {
                aim.ResetRotation();
                FlipX = input.Horizontal < 0;
            }
        }
    }
}