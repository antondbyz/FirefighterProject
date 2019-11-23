using System.Collections;
using UnityEngine;

public class Spark : MonoBehaviour
{
    [HideInInspector] public Fire ParentFire;

    [SerializeField] private Fire newFire;
    private WaitForSeconds enableCollisionDelay = new WaitForSeconds(0.2f);
    private WaitForSeconds destroySparkDelay = new WaitForSeconds(5f);
    private bool checkCollisions = false;

    private void Start()
    {
        StartCoroutine(SparkCreated());
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(checkCollisions)
        {
            if(other.gameObject.layer == GameManager.Layers.GROUND)
            {
                Fire fire = Instantiate(newFire, transform.position, Quaternion.identity);
                fire.HealthModule.Damageable.SetHealth(1);
            }
            else if(other.gameObject.TryGetComponent(out Burnable burnable))
            {
                burnable.StartBurning(ParentFire.DamageModule);
            }
            else if(other.gameObject.TryGetComponent(out FireHealth fireHealth))
            {
                fireHealth.Damageable.ToTreat(10);
            }
            Destroy(gameObject);
        }
    }

    private IEnumerator SparkCreated()
    {
        yield return enableCollisionDelay;
        checkCollisions = true; 
        yield return destroySparkDelay;
        Destroy(gameObject);
    }
}
