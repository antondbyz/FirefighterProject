using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerController controller;
    private PlayerAim aim;
    private int runningAnimation;
    private int aimingAnimation;
    private int holdingLedgeAnimation;
    private int jumpingAnimation;
    private int fallingAnimation;

    private void Awake() 
    {
        animator = transform.parent.GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        aim = GetComponent<PlayerAim>();
        runningAnimation = Animator.StringToHash("Running");   
        holdingLedgeAnimation = Animator.StringToHash("HoldingLedge");
        aimingAnimation = Animator.StringToHash("Aiming"); 
        jumpingAnimation = Animator.StringToHash("Jumping");
        fallingAnimation = Animator.StringToHash("Falling");
    }

    private void Update() 
    {
        if(Input.GetKeyDown(KeyCode.R)) Time.timeScale -= 0.1f;
        else if(Input.GetKeyDown(KeyCode.T)) Time.timeScale += 0.1f;
        bool playerGrounded = controller.IsGrounded;
        animator.SetBool(runningAnimation, controller.NewVelocity.x != 0);
        animator.SetBool(holdingLedgeAnimation, controller.IsHoldingLedge);
        animator.SetBool(aimingAnimation, aim.IsAiming);
        animator.SetBool(jumpingAnimation, !playerGrounded && controller.NewVelocity.y > 0.01f);
        animator.SetBool(fallingAnimation, !playerGrounded && controller.NewVelocity.y <= 0);
    }
}