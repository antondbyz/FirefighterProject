using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    public static bool IsInstructionShown = false;

    [SerializeField] private GameObject instructionUI = null;
    [SerializeField] private GameObject player = null;

    private void Awake() 
    {
        if(!IsInstructionShown) 
        {
            player.SetActive(false);
            instructionUI.SetActive(true);
        }
    }

    public void FinishInstruction()
    {
        IsInstructionShown = true;
        player.SetActive(true);
        instructionUI.SetActive(false);
    }
}