using UnityEngine;

public abstract class BackdraftObstacle : MonoBehaviour
{
    public event System.Action ObstacleDisappeared;

    protected void InvokeObstacleDisappeared() => ObstacleDisappeared?.Invoke();    
}