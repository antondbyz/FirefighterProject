using UnityEngine;

public class InstructionManager : MonoBehaviour
{
    private void Awake() 
    {
        GameController.Instance.IsPaused = true;
    }

    public void FinishInstruction()
    {
        GameController.Instance.IsPaused = false;
        gameObject.SetActive(false);
    }
}