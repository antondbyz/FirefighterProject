using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerController controller;
    private PlayerAiming aiming;
    private int runningAnimation;
    private int aimingAnimation;

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        aiming = GetComponent<PlayerAiming>();
        runningAnimation = Animator.StringToHash("Running");  
        aimingAnimation = Animator.StringToHash("Aiming");  
    }

    private void Update() 
    {
        animator.SetBool(runningAnimation, controller.IsMoving);
        animator.SetBool(aimingAnimation, aiming.IsAiming);
    }
}