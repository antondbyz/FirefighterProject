using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    private enum InputType { KEYBOARD, BUTTONS }

    [SerializeField] private InputType inputType = InputType.KEYBOARD;
    [Space]
    [Header("Classes")]
    [SerializeField] private PlayerController controller;
    [SerializeField] private ExtinguishingSubstance substance;
    [SerializeField] private Medic medic;
    [Space]
    [Header("Buttons")]
    [SerializeField] private CustomButton moveRightButton = null;
    [SerializeField] private CustomButton moveLeftButton = null;
    [SerializeField] private CustomButton jumpButton = null;
    [SerializeField] private CustomButton extinguishButton = null;
    [SerializeField] private CustomButton treatButton = null;

    private void OnEnable() 
    {
        if(inputType == InputType.BUTTONS)
        {
            jumpButton.PointerDown += controller.Jump;    
        }
    }

    private void OnDisable() 
    {
        jumpButton.PointerDown -= controller.Jump;   
    }

    private void Update() 
    {
        switch(inputType)
        {
            case InputType.KEYBOARD:
                if(Input.GetKeyDown(KeyCode.UpArrow)) controller.Jump();

                controller.SetVelocityX(Input.GetAxisRaw("Horizontal"));

                if(Input.GetKeyDown(KeyCode.E)) substance.TurnOn();
                else if(Input.GetKeyUp(KeyCode.E)) substance.TurnOff();

                if(Input.GetKeyDown(KeyCode.M)) medic.StartTreating();
                else if(Input.GetKeyUp(KeyCode.M)) medic.StopTreating();
                break;
            case InputType.BUTTONS:
                if(moveRightButton.Hold ^ moveLeftButton.Hold)
                {
                    if(moveRightButton.Hold) controller.SetVelocityX(1);
                    else controller.SetVelocityX(-1);
                }
                else controller.SetVelocityX(0);
                
                if(extinguishButton.Hold) substance.TurnOn();
                else substance.TurnOff();

                if(treatButton.Hold) medic.StartTreating();
                else medic.StopTreating();
                break;
        }
    }
}