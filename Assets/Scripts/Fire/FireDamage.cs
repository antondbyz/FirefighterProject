using UnityEngine;

public class FireDamage : MonoBehaviour
{
    private Fire fire;
    public float Damage { get; private set; }
    [HideInInspector] public FloatEvent OnDamageChanged;

    private void Awake()
    {
        fire = transform.parent.GetComponent<Fire>();
    }

    private void Start()
    {
        OnDamageChanged = new FloatEvent();
        fire.HealthModule.Damageable.OnHealthChanged.AddListener(UpdateDamage);
        UpdateDamage(fire.HealthModule.Damageable.HealthPercentage);
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

    private void UpdateDamage(float healthPercentage)
    {
        float newDamage = healthPercentage.PercentageToNumber(Fire.Constraints.MIN_DAMAGE, Fire.Constraints.MAX_DAMAGE);
        OnDamageChanged.Invoke(newDamage - Damage);
        Damage = newDamage;
    }
}