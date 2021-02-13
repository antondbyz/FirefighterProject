using UnityEngine;

public class BreakableObstacle : MonoBehaviour
{
    public event System.Action Broke;

    [SerializeField] private Pool brokenVersionsPool = null;
    [SerializeField] private float brokenVersionLifetime = 2;

    private Transform myTransform;

    private void Awake() => myTransform = transform;

    public void Break(Vector2 force)
    {
        GameObject newBrokenObj = ObjectPooler.Instance.SpawnObject(brokenVersionsPool, myTransform.position);
        newBrokenObj.GetComponent<Rigidbody2D>().AddForce(force);
        GameController.Instance.DoSomething(() => 
        {
            newBrokenObj.SetActive(false);
        }, brokenVersionLifetime);
        Broke?.Invoke();
        Destroy(gameObject);
    }
}