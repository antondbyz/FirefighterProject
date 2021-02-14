using UnityEngine;

public class BreakableObstacle : Breakable
{
    [SerializeField] private Pool brokenVersionsPool = null;
    [SerializeField] private float brokenVersionLifetime = 2;

    private Transform myTransform;

    private void Awake() => myTransform = transform;

    public void Break(Vector2 force)
    {
        GameObject newBrokenObj = brokenVersionsPool.SpawnObject(myTransform.position);
        newBrokenObj.GetComponent<Rigidbody2D>().AddForce(force);
        GameController.Instance.DoSomething(() => 
        {
            newBrokenObj.SetActive(false);
        }, brokenVersionLifetime);
        InvokeBroke();
        Destroy(gameObject);
    }
}