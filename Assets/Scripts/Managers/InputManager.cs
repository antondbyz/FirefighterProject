using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static bool ExtinguishHeld { get; private set; }
    public static event System.Action JumpPressed;

    [SerializeField] private CustomButton moveRightButton = null;
    [SerializeField] private CustomButton moveLeftButton = null;
    [SerializeField] private CustomButton extinguishButton = null;
    [SerializeField] private CustomButton jumpButton = null;

    private void OnEnable() => jumpButton.Pressed += InvokeJumpPressed;

    private void OnDisable() => jumpButton.Pressed -= InvokeJumpPressed;
    
    private void Update() 
    {
        #if UNITY_EDITOR
        CheckKeyboardInput();
        #else
        CheckCustomButtonsInput();
        #endif
    }

    private void InvokeJumpPressed() => JumpPressed?.Invoke();

    private void CheckCustomButtonsInput()
    {
        if(moveRightButton.Hold ^ moveLeftButton.Hold)
        {
            if(moveRightButton.Hold) Horizontal = 1;
            else Horizontal = -1;
        }
        else Horizontal = 0;
        ExtinguishHeld = extinguishButton.Hold;
    }

    private void CheckKeyboardInput()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        ExtinguishHeld = Input.GetKey(KeyCode.E);
        if(Input.GetKeyDown(KeyCode.UpArrow)) JumpPressed?.Invoke();
    }
}