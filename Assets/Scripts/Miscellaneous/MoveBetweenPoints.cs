using UnityEngine;

public class MoveBetweenPoints : MonoBehaviour
{
    [SerializeField] private Transform[] points = null;
    [SerializeField] private float speed = 10;

    private Vector2 nextPoint;
    private Transform myTransform;
    private int nextPointIndex = 0;

    private void Awake() 
    {
        nextPoint = points[nextPointIndex].position;
        myTransform = transform;    
    }

    private void Update() 
    {
        myTransform.position = Vector2.MoveTowards(myTransform.position, nextPoint, speed * Time.deltaTime);
        if((Vector2)myTransform.position == nextPoint)
        {
            nextPointIndex++;
            if(nextPointIndex >= points.Length) nextPointIndex = 0;
            nextPoint = points[nextPointIndex].position;
        }
    }
}