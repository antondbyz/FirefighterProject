using UnityEngine;

public class PlayerAnimations : MonoBehaviour
{
    private Animator animator;
    private PlayerController controller;
    private Extinguisher extinguisher;
    private int runningAnimation;
    private int extinguishingAnimation;
    private int holdingLedgeAnimation;

    private void Awake() 
    {
        animator = transform.parent.GetComponent<Animator>();
        controller = GetComponent<PlayerController>();
        extinguisher = transform.GetChild(0).GetComponent<Extinguisher>();
        runningAnimation = Animator.StringToHash("Running");   
        holdingLedgeAnimation = Animator.StringToHash("HoldingLedge");
        extinguishingAnimation = Animator.StringToHash("Extinguishing"); 
    }

    private void Update() 
    {
        animator.SetBool(runningAnimation, controller.IsMoving);
        animator.SetBool(holdingLedgeAnimation, controller.IsHoldingLedge);
        animator.SetBool(extinguishingAnimation, extinguisher.IsTurnedOn);
    }
}