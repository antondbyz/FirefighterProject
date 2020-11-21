using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 Direction => myTransform.right;
    public Vector2 ColliderCenter => bc.bounds.center;

    private Transform myTransform;
    private BoxCollider2D bc;
    private Vector2 currentCheckpoint;

    public void MoveToCurrentCheckpoint() => myTransform.position = currentCheckpoint;

    private void Awake() 
    {
        myTransform = transform;  
        bc = GetComponent<BoxCollider2D>();
        currentCheckpoint = myTransform.position;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Victim")) Destroy(other.gameObject);
        else if(other.CompareTag("Finish")) GameController.Instance.CompleteLevel();
    }
}