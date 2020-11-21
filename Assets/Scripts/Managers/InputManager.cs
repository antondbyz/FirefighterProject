using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static bool ExtinguishHeld { get; private set; }
    public static event System.Action JumpPressed;
    public static event System.Action HitPressed;

    [SerializeField] private CustomButton moveRightButton = null;
    [SerializeField] private CustomButton moveLeftButton = null;
    [SerializeField] private CustomButton jumpButton = null;
    [SerializeField] private CustomButton extinguishButton = null;
    [SerializeField] private CustomButton hitButton = null;

    private void OnEnable() 
    { 
        jumpButton.Pressed += JumpButtonPressed;
        hitButton.Pressed += HitButtonPressed;
    }

    private void OnDisable() 
    {
        jumpButton.Pressed -= JumpButtonPressed;
        hitButton.Pressed -= HitButtonPressed; 
    }

    private void Update() 
    {
        #if UNITY_EDITOR
        ProcessKeyboardInput();
        #else 
        ProcessCustomButtonsInput();
        #endif
    }

    private void ProcessKeyboardInput()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        ExtinguishHeld = Input.GetKey(KeyCode.E);
        if(Input.GetKeyDown(KeyCode.UpArrow)) JumpPressed?.Invoke();
        if(Input.GetKeyDown(KeyCode.H)) HitPressed?.Invoke();
    }

    private void ProcessCustomButtonsInput()
    {
        if(moveRightButton.Held == moveLeftButton.Held) Horizontal = 0;
        else if(moveRightButton.Held) Horizontal = 1;
        else Horizontal = -1;
        ExtinguishHeld = extinguishButton.Held;
    }

    private void JumpButtonPressed() 
    { 
        #if !UNITY_EDITOR
        JumpPressed?.Invoke();
        #endif
    }

    private void HitButtonPressed() 
    {
        #if !UNITY_EDITOR 
        HitPressed?.Invoke();
        #endif
    }
}