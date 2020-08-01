using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float VelocityX
    {
        set
        {
            if(!aiming.IsAiming)
            {
                newVelocity.x = value * speed;
                if(value > 0) FlipX = false;
                else if(value < 0) FlipX = true;
                animator.SetBool("Running", value != 0);
                if(value != 0) aiming.ResetRotation();
            }
        }
    }
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

    public bool IsGrounded
    {
        get
        {
            float distance = cc.size.y / 2 - cc.size.x / 2 + groundCheckDistance;
            return Physics2D.CircleCast(cc.bounds.center, cc.size.x / 2, Vector2.down, distance);
        }
    }  
    public bool IsMoving => rb.velocity.x != 0 || rb.velocity.y != 0;

    [SerializeField] private float speed = 5;
    [SerializeField] private float jumpForce = 250;
    [SerializeField] private float groundCheckDistance = 0.05f;
    
    private Transform myTransform;
    private Rigidbody2D rb;
    private CapsuleCollider2D cc;
    private Animator animator;
    private PlayerAiming aiming;
    private Vector2 newVelocity;

    public void Jump()
    {
        if(!aiming.IsAiming && IsGrounded)
           rb.AddForce(Vector2.up * jumpForce);
    }

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        cc = GetComponent<CapsuleCollider2D>();
        animator = GetComponent<Animator>();
        aiming = GetComponent<PlayerAiming>();
    }

#if UNITY_EDITOR
    private void Update() 
    {
        VelocityX = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.UpArrow)) Jump();
    }
#endif 

    private void FixedUpdate() 
    {
        SlopeCheck();
        rb.velocity = newVelocity;
    }

    private void SlopeCheck()
    {
        if(IsGrounded)
        {
            Vector2 rayOrigin = new Vector2(cc.bounds.center.x, cc.bounds.center.y - cc.size.y / 2);
            Vector2 rayOriginHorizontal = rayOrigin + new Vector2(0, 0.2f);
            RaycastHit2D hitDown = Physics2D.Raycast(rayOrigin, Vector2.down); 
            RaycastHit2D hitFront = Physics2D.Raycast(rayOriginHorizontal, Vector2.right, 0.5f);
            RaycastHit2D hitBack = Physics2D.Raycast(rayOriginHorizontal, Vector2.left, 0.5f);
            if(Vector2.Angle(hitDown.normal, Vector2.up) > 0)
            {
                float dot = Vector2.Dot(hitDown.normal, Vector2.right);
                if((newVelocity.x > 0 && dot < 0 && !hitFront)) newVelocity.y = 0;
                else if((newVelocity.x < 0 && dot > 0 && !hitBack)) newVelocity.y = 0;
                else
                {
                    Vector2 slopeNormalPerpendicular = Vector2.Perpendicular(hitDown.normal).normalized;
                    newVelocity.Set(newVelocity.x * -slopeNormalPerpendicular.x, newVelocity.x * -slopeNormalPerpendicular.y);
                }
            }
            else newVelocity.y = 0;
        }
        else newVelocity.y = rb.velocity.y;  
    }
}