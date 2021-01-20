using UnityEngine;

public class BreakableDoor : BreakableObject
{
    [SerializeField] private GameObject brokenVersion = null;
    [SerializeField] private float brokenVersionLifetime = 2;

    public void Break(Vector2 force)
    {
        GameObject newBrokenObj = Instantiate(brokenVersion, transform.position, Quaternion.identity);
        newBrokenObj.GetComponent<Rigidbody2D>().AddForce(force);
        GameController.DestroyWithDelay(newBrokenObj, brokenVersionLifetime);
        BreakObject();
    }
}