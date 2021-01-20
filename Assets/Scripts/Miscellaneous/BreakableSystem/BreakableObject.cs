using UnityEngine;

public abstract class BreakableObject : MonoBehaviour
{
    public event System.Action Broken;

    protected void BreakObject()
    {
        Broken?.Invoke();
        Destroy(gameObject);
    }
}