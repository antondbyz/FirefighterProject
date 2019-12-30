using UnityEngine;

public class Fire : MonoBehaviour
{
    public FireSettings Settings => settings;
    public FireHealth FireHealth => fireHealth;
    public SparksSpawner SparksSpawner => sparksSpawner;
    public float Damage { get; private set; }
    public FloatEvent OnDamageChanged { get; private set; }

    [SerializeField] private FireSettings settings = null;
    [SerializeField] private FireHealth fireHealth = null;
    [SerializeField] private SparksSpawner sparksSpawner = null;

    private void Start()
    {
        OnDamageChanged = new FloatEvent();
        fireHealth.OnHealthChanged.AddListener(UpdateFire);
        UpdateFire(fireHealth.HealthPercentage);
        StartCoroutine(sparksSpawner.SpawnSparks(this));
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out Burnable burnable))
        {
            burnable.StartBurning(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out Burnable burnable))
        {
            burnable.StopBurning(this);
        }
    }

    private void UpdateFire(float healthPercentage)
    {
        // size update
        float newSize = healthPercentage.PercentageToNumberInRange(settings.Size.MinSize, 
                                                                   settings.Size.MaxSize);
        transform.localScale = new Vector2(newSize, newSize);
        // damage update
        float newDamage = healthPercentage.PercentageToNumberInRange(settings.Damage.MinDamage,
                                                                     settings.Damage.MaxDamage);
        OnDamageChanged.Invoke(newDamage - Damage);
        Damage = newDamage;
        // sparks spawning update
        float newFrequency = healthPercentage.PercentageToNumberInRange(settings.Sparks.MinFrequency,
                                                                        settings.Sparks.MaxFrequency); 
        float newYForce = healthPercentage.PercentageToNumberInRange(settings.Sparks.MinYForce,
                                                                     settings.Sparks.MaxYForce);
        float newXForce = healthPercentage.PercentageToNumberInRange(settings.Sparks.MinXForce,
                                                                     settings.Sparks.MaxXForce);           
        sparksSpawner.UpdateSparksSpawning(newFrequency, newYForce, newXForce);                                                                                                                                                               
    }
}