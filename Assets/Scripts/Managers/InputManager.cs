using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static float Horizontal { get; private set; }
    public static bool JumpPressed { get; private set; }
    public static bool HitPressed { get; private set; }

    [SerializeField] private CustomButton moveRightButton = null;
    [SerializeField] private CustomButton moveLeftButton = null;
    [SerializeField] private CustomButton jumpButton = null;

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
    }

    private void ProcessCustomButtonsInput()
    {
        if(moveRightButton.Held == moveLeftButton.Held) Horizontal = 0;
        else if(moveRightButton.Held) Horizontal = 1;
        else Horizontal = -1;
        JumpPressed = jumpButton.Pressed;
    }
}