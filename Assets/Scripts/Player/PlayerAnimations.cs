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
        GameObject playerCharacter = transform.GetChild(0).gameObject;
        controller = playerCharacter.GetComponent<PlayerController>();
        aim = playerCharacter.GetComponent<PlayerAim>();
        animator = GetComponent<Animator>();
        runningAnimation = Animator.StringToHash("Running");  
        aimingAnimation = Animator.StringToHash("Aiming");  
        holdingLedgeAnimation = Animator.StringToHash("HoldingLedge");
    }

    private void Update() 
    {
        animator.SetBool(runningAnimation, controller.IsMoving);
        animator.SetBool(aimingAnimation, aim.IsAiming);
        animator.SetBool(holdingLedgeAnimation, controller.IsHoldingLedge);
    }
}