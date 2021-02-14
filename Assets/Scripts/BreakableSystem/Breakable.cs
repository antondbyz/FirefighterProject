using UnityEngine;

public abstract class Breakable : MonoBehaviour
{
    public event System.Action Broke;

    protected void InvokeBroke() => Broke?.Invoke();    
}