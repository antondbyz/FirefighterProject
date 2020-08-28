using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;
    private GameController gameController;
    private ExtinguishingSubstance extinguishingSubstance;
    private Vector2 lastCheckpoint;

    public void PauseLevel() => gameController.PauseLevel();

    public void MoveToLastCheckpoint() => rb.position = lastCheckpoint;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        gameController = GameObject.FindObjectOfType<GameController>();    
        extinguishingSubstance = transform.GetComponentInChildren<ExtinguishingSubstance>();
        lastCheckpoint = rb.position;
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