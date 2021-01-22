using UnityEngine;

public class ElectricBall : MonoBehaviour
{
    [SerializeField] private Transform a = null;
    [SerializeField] private Transform b = null;
    [SerializeField] private float speed = 10;

    private Vector2 nextPoint;
    private Vector2 pointA;
    private Vector2 pointB;

    private void Awake() 
    {
        pointA = a.position;
        pointB = b.position;
        nextPoint = pointA;    
    }

    private void Update() 
    {
        transform.position = Vector2.MoveTowards(transform.position, nextPoint, speed * Time.deltaTime);
        if((Vector2)transform.position == nextPoint) 
            nextPoint = nextPoint == (Vector2)pointA ? pointB : pointA;
    }
}