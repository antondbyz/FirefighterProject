using UnityEngine;

public class Spark : MonoBehaviour
{
    [Tooltip("When a collision occurs with these layers, the spark deactivates")]
    [SerializeField] private LayerMask collidesWith = new LayerMask();
    //[SerializeField] private Fire firePrefab = null;
    //public Fire ParentFire { get; private set; }
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    //public void Initialize(Fire parentFire) { ParentFire = parentFire; }

    public void AddForce(Vector2 direction) { rb.AddForce(direction); }

    // private void OnTriggerEnter2D(Collider2D other)
    // {
    //     if(collidesWith == (collidesWith | (1 << other.gameObject.layer)))
    //     {
    //         if(other.gameObject.layer == LayerMask.NameToLayer("Ground"))
    //         {
    //             Fire fire = Instantiate(firePrefab, transform.position, Quaternion.identity);
    //             fire.FireHealth.CurrentHealth = 1;
    //         }
    //         else if(other.TryGetComponent(out Fire fire))
    //         {
    //             if(fire == ParentFire) return;
    //             fire.FireHealth.CurrentHealth = ParentFire.Damage;
    //         }
    //         gameObject.SetActive(false);
    //     }
    // }
}