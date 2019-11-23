using UnityEngine;

public class Fire : MonoBehaviour
{
    public struct Constraints
    {
        public const float MAX_SIZE = 1, MIN_SIZE = 0.1f;
        public const float MAX_DAMAGE = 1f, MIN_DAMAGE = 0.2f;
    }

    public FireHealth HealthModule { get; private set; }
    public FireDamage DamageModule { get; private set; }

    private void Awake()
    {
        HealthModule = transform.GetChild(0).GetComponent<FireHealth>();
        DamageModule = transform.GetChild(1).GetComponent<FireDamage>();
    }

    private void Start()
    {
        HealthModule.Damageable.OnHealthChanged.AddListener(UpdateSize);
        UpdateSize(HealthModule.Damageable.HealthPercentage);
    }
    
    private void UpdateSize(float healthPercentage)
    {
        float newSize = healthPercentage.PercentageToNumber(Constraints.MIN_SIZE, Constraints.MAX_SIZE);
        transform.localScale = new Vector2(newSize, newSize);
    }
}