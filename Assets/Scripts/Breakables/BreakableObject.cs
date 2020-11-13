using UnityEngine;

public class BreakableObject : MonoBehaviour 
{
    public event System.Action Broken;

    [SerializeField] private BrokenObject brokenVersion = null;
    [SerializeField] private float breakForce = 300;
    [SerializeField] private float breakTorque = 100;

    public void Break(Vector2 direction)
    {
        Transform brokenObject = Instantiate(brokenVersion, transform.position, Quaternion.identity).transform;
        for(int i = 0, n = brokenObject.childCount; i < n; i++)
        {
            Rigidbody2D rb = brokenObject.GetChild(i).GetComponent<Rigidbody2D>();
            rb.AddForce(direction * breakForce);
            rb.AddTorque(breakTorque * direction.x);
        }
        Broken?.Invoke();
        Destroy(gameObject);
    }    
}