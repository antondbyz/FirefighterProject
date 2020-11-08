using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static bool ExtinguishHeld { get; private set; }
    public static event System.Action JumpPressed;
    public static event System.Action HitPressed;

    [SerializeField] private CustomButton moveRightButton = null;
    [SerializeField] private CustomButton moveLeftButton = null;
    [SerializeField] private CustomButton extinguishButton = null;
    [SerializeField] private CustomButton jumpButton = null;
    [SerializeField] private CustomButton hitButton = null;

    private void OnEnable() 
    {
        jumpButton.Pressed += InvokeJumpPressed;
        hitButton.Pressed += InvokeHitPressed;
    }

    private void OnDisable() 
    { 
        jumpButton.Pressed -= InvokeJumpPressed;
        hitButton.Pressed -= InvokeHitPressed;
    }

    private void Update() 
    {
        #if UNITY_EDITOR
        CheckKeyboardInput();
        #else
        CheckCustomButtonsInput();
        #endif
    }

    private void InvokeJumpPressed() => JumpPressed?.Invoke();

    private void InvokeHitPressed() => HitPressed?.Invoke();

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
        if(Input.GetKeyDown(KeyCode.H)) HitPressed?.Invoke();
    }
}