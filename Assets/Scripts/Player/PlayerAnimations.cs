using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerController controller;
    private PlayerAim aim;
    private int runningAnimation;
    private int aimingAnimation;
    private int holdingLedgeAnimation;

    private void Awake() 
    {
        animator = transform.parent.GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        aim = GetComponent<PlayerAim>();
        runningAnimation = Animator.StringToHash("Running");   
        holdingLedgeAnimation = Animator.StringToHash("HoldingLedge");
        aimingAnimation = Animator.StringToHash("Aiming"); 
    }

    private void Update() 
    {
        animator.SetBool(runningAnimation, controller.IsMoving);
        animator.SetBool(holdingLedgeAnimation, controller.IsHoldingLedge);
        animator.SetBool(aimingAnimation, aim.IsAiming);
    }
}