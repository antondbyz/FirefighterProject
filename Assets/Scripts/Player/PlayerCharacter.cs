using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    private Transform myTransform;
    private Vector2 currentCheckpoint;

    public void MoveToCurrentCheckpoint() => myTransform.position = currentCheckpoint;

    private void Awake() 
    {
        myTransform = transform;  
        currentCheckpoint = myTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Victim")) Destroy(other.gameObject);
        else if(other.CompareTag("Finish")) GameController.Instance.CompleteLevel();
    }
}