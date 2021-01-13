using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static bool JumpPressed { get; private set; }
    public static bool ExtinguishHeld { get; private set; }
    public static bool HitPressed { get; private set; }

    [SerializeField] private CustomButton moveRightButton = null;
    [SerializeField] private CustomButton moveLeftButton = null;
    [SerializeField] private CustomButton jumpButton = null;
    [SerializeField] private CustomButton extinguishButton = null;
    [SerializeField] private CustomButton hitButton = null;

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
        JumpPressed = Input.GetKeyDown(KeyCode.W);
        ExtinguishHeld = Input.GetKey(KeyCode.E);
        HitPressed = Input.GetKeyDown(KeyCode.F);
    }

    private void ProcessCustomButtonsInput()
    {
        if(moveRightButton.Held == moveLeftButton.Held) Horizontal = 0;
        else if(moveRightButton.Held) Horizontal = 1;
        else Horizontal = -1;
        JumpPressed = jumpButton.Pressed;
        ExtinguishHeld = extinguishButton.Held;
        HitPressed = hitButton.Pressed;
    }
}