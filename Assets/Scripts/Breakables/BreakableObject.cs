using UnityEngine;

public class BreakableObject : MonoBehaviour 
{
    [SerializeField] private BrokenObject brokenVersion = null;
    [SerializeField] private float breakForce = 300;
    [SerializeField] private float breakTorque = 100;

    public void Break(Vector2 direction)
    {
        Transform newBrokenVersion = Instantiate(brokenVersion, transform.position, Quaternion.identity).transform;
        for(int i = 0, n = newBrokenVersion.childCount; i < n; i++)
        {
            Rigidbody2D rb = newBrokenVersion.GetChild(i).GetComponent<Rigidbody2D>();
            rb.AddForce(direction * breakForce);
            rb.AddTorque(breakTorque * direction.x);
        }
        Destroy(gameObject);
    }    
}