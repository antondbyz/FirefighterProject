using UnityEngine;

public class BackdraftObstacle : MonoBehaviour
{
    public event System.Action ObstacleDisappeared;

    public void InvokeObstacleDisappeared() => ObstacleDisappeared?.Invoke();    
}