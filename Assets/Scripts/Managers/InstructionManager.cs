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
            GameController.Instance.IsPaused = true;
            player.SetActive(false);
            instructionUI.SetActive(true);
            IsInstructionShown = true;
        }
    }
}