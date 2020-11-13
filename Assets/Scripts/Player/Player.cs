using UnityEngine;

public class Player : MonoBehaviour
{
    public Vector2 CurrentDirection => myTransform.right;

    private Transform myTransform;
    private BoxCollider2D bc;
    private Vector2 currentCheckpoint;

    public RaycastHit2D WhatIsInFront(float distance, int mask = -1)
    {
        return Physics2D.Raycast(bc.bounds.center, myTransform.right, bc.size.x / 2 + distance, mask);
    }

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
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Finish")) GameController.Instance.CompleteLevel();
    }
}