using UnityEngine;

public class BreakableObject : MonoBehaviour 
{
    public event System.Action Broken;

    [SerializeField] private BrokenObject brokenVersion = null;

    public void Break(Vector2 direction, float force)
    {
        Transform newBrokenVersion = Instantiate(brokenVersion, transform.position, Quaternion.identity).transform;
        for(int i = 0, n = newBrokenVersion.childCount; i < n; i++)
        {
            Rigidbody2D rb = newBrokenVersion.GetChild(i).GetComponent<Rigidbody2D>();
            rb.AddForce(direction * force);
            rb.AddTorque(force / 3 * direction.x);
        }
        Broken?.Invoke();
        Destroy(gameObject);
    }    
}