using System.Collections;
using UnityEngine;

public class SparksCreator : MonoBehaviour
{
    public struct Constraints
    {
        public const float MAX_SPARK_UP_FORCE = 300f, MIN_SPARK_UP_FORCE = 200f;
        public const float MAX_SPARK_SIDE_FORCE = 100f, MIN_SPARK_SIDE_FORCE = 50f;
        public const float MAX_DELAY_BETWEEN_SPARKS = 5f, MIN_DELAY_BETWEEN_SPARKS = 1f;
    }

    [SerializeField] private Rigidbody2D spark = null;
    private Fire fire;
    private float sparkUpForce, sparkSideForce, delayBetweenSparks;

    private void Awake()
    {
        fire = GetComponent<Fire>();
    }

    private void Start()
    {
        fire.HealthModule.Damageable.OnHealthChanged.AddListener(UpdateSpark);
        UpdateSpark(fire.HealthModule.Damageable.HealthPercentage);
        StartCoroutine(CreateSparks());
    }

    private IEnumerator CreateSparks()
    {
        while(true)
        {
            yield return new WaitForSeconds(delayBetweenSparks);
            Rigidbody2D newSpark = Instantiate(spark, transform.position, Quaternion.identity);
            newSpark.GetComponent<Spark>().ParentFire = fire;

            newSpark.AddForce(Vector2.up * sparkUpForce);
            Vector2 side = Random.Range(0f, 1f) > 0.5f ? Vector2.right : Vector2.left;
            newSpark.AddForce(side * sparkSideForce);
        }
    }

    private void UpdateSpark(float healthPercentage)
    {
        sparkUpForce = healthPercentage.PercentageToNumber(Constraints.MIN_SPARK_UP_FORCE, Constraints.MAX_SPARK_UP_FORCE);
        sparkSideForce = healthPercentage.PercentageToNumber(Constraints.MIN_SPARK_SIDE_FORCE, Constraints.MAX_SPARK_SIDE_FORCE);
        delayBetweenSparks = healthPercentage.PercentageToNumber(Constraints.MAX_DELAY_BETWEEN_SPARKS, 
                                                                 Constraints.MIN_DELAY_BETWEEN_SPARKS);
    }
}