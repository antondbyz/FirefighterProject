using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private bool keyboardInput = true;
    [Space]
    [Header("Buttons")]
    [SerializeField] private CustomButton moveRightButton = null;
    [SerializeField] private CustomButton moveLeftButton = null;
    [SerializeField] private CustomButton jumpButton = null;
    [SerializeField] private CustomButton extinguishButton = null;
    [SerializeField] private CustomButton treatButton = null;

    private PlayerController controller;
    private ExtinguishingSubstance substance;

    private void Awake() 
    {
        controller = GetComponent<PlayerController>();    
        substance = transform.GetComponentInChildren<ExtinguishingSubstance>();
    }

    private void OnEnable() 
    {
        if(!keyboardInput)
        {
            jumpButton.PointerDown += controller.Jump;    
        }
    }

    private void OnDisable() 
    {
        if(!keyboardInput)
        {
            jumpButton.PointerDown -= controller.Jump; 
        }   
    }

    private void Update() 
    {
        if(keyboardInput)
        {
            if(Input.GetKeyDown(KeyCode.UpArrow)) controller.Jump();

            controller.SetVelocityX(Input.GetAxisRaw("Horizontal"));
            
            if(Input.GetKeyDown(KeyCode.E)) substance.TurnOn();
            else if(Input.GetKeyUp(KeyCode.E)) substance.TurnOff();
        }
        else
        {
            if(moveRightButton.Hold ^ moveLeftButton.Hold)
            {
                if(moveRightButton.Hold) controller.SetVelocityX(1);
                else controller.SetVelocityX(-1);
            }
            else controller.SetVelocityX(0);
            
            if(extinguishButton.Hold) substance.TurnOn();
            else substance.TurnOff();
        }
    }
}