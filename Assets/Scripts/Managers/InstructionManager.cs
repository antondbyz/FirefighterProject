using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    [SerializeField] private GameObject instructionUI = null;
    [SerializeField] private GameObject player = null;

    private void Awake() 
    {
        player.SetActive(false);
        instructionUI.SetActive(true);
    }

    public void FinishInstruction()
    {
        player.SetActive(true);
        instructionUI.SetActive(false);
    }
}