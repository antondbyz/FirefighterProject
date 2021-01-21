using UnityEngine;

public class BreakableObstacle : MonoBehaviour
{
    public event System.Action Broke;

    [SerializeField] private Rigidbody2D brokenVersion = null;
    [SerializeField] private float brokenVersionLifetime = 2;

    public void Break(Vector2 force)
    {
        Rigidbody2D newBrokenObj = Instantiate(brokenVersion, transform.position, Quaternion.identity);
        newBrokenObj.AddForce(force);
        GameController.DestroyWithDelay(newBrokenObj.gameObject, brokenVersionLifetime);
        Broke?.Invoke();
        Destroy(gameObject);
    }
}