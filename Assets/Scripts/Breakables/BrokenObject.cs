using UnityEngine;

public class BrokenObject : MonoBehaviour 
{
    [SerializeField] private float lifetime = 1;

    private void Awake() 
    {
        Destroy(gameObject, lifetime);
    }
}