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
        animator.SetBool(runningAnimation, controller.NewVelocity.x != 0);
        animator.SetBool(holdingLedgeAnimation, controller.IsHoldingLedge);
        animator.SetBool(aimingAnimation, aim.IsAiming);
        animator.SetBool(jumpingAnimation, !controller.IsGrounded && controller.NewVelocity.y > 0.01f);
        animator.SetBool(fallingAnimation, !controller.IsGrounded && controller.NewVelocity.y <= 0);
    }
}