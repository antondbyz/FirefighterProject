using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public bool JumpPressed { get; private set; }
    public bool MedicHeld { get; private set; }
    public bool ExtinguishHeld { get; private set; }

    [SerializeField] private CustomButton moveRightButton = null;
    [SerializeField] private CustomButton moveLeftButton = null;
    [SerializeField] private CustomButton jumpButton = null;
    [SerializeField] private CustomButton extinguishButton = null;
    [SerializeField] private CustomButton treatButton = null;

    public void CheckCustomButtonsInput()
    {
        if(moveRightButton.Hold ^ moveLeftButton.Hold)
        {
            if(moveRightButton.Hold) Horizontal = 1;
            else Horizontal = -1;
        }
        else Horizontal = 0;
        JumpPressed = jumpButton.Pressed;
        MedicHeld = treatButton.Hold;
        ExtinguishHeld = extinguishButton.Hold;
    }

    public void CheckKeyboardInput()
    {
        Horizontal = Input.GetAxisRaw("Horizontal");
        if(Input.GetKeyDown(KeyCode.UpArrow)) JumpPressed = true;
        MedicHeld = Input.GetKey(KeyCode.M);
        ExtinguishHeld = Input.GetKey(KeyCode.E);

    }

    private void Update() 
    {
        //#if UNITY_EDITOR
        CheckKeyboardInput();
        //#else
        //CheckCustomButtonsInput();
        //#endif
    }

    private void FixedUpdate() 
    {
        JumpPressed = false;
    }
}