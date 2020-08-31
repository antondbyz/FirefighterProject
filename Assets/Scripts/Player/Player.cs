using UnityEngine;

public class Player : MonoBehaviour
{
    private Transform myTransform;
    private GameController gameController;
    private ExtinguishingSubstance extinguishingSubstance;
    private Vector2 currentCheckpoint;

    public void PauseLevel() => gameController.PauseLevel();

    public void MoveToCurrentCheckpoint() => myTransform.position = currentCheckpoint;

    private void Awake() 
    {
        myTransform = transform;
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();    
        extinguishingSubstance = myTransform.GetComponentInChildren<ExtinguishingSubstance>();
        currentCheckpoint = myTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("ExtinguisherRefill"))
        {
            extinguishingSubstance.CurrentSubstanceAmount = ExtinguishingSubstance.MAX_SUBSTANCE_AMOUNT;
            Destroy(other.gameObject);
        }
        else if(other.CompareTag("Finish")) gameController.CompleteLevel();
    }
}