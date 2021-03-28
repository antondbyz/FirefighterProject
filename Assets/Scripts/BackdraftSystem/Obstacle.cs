using UnityEngine;

public class Obstacle : MonoBehaviour
{
    public event System.Action Disappeared;

    public void InvokeObstacleDisappeared() => Disappeared?.Invoke();    
}