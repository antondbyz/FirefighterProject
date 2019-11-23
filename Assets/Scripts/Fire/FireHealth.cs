using System.Collections;
using UnityEngine;

public class FireHealth : MonoBehaviour
{
    public Damageable Damageable { get; private set; }

    private float receivingDamage;
    private Fire fire;
    private Emitting steam; 
    private WaitForSeconds wait = new WaitForSeconds(0.1f);

    private void Awake()
    {
        Damageable = GetComponent<Damageable>();
        fire = transform.parent.GetComponent<Fire>();
        steam = transform.GetChild(0).GetComponent<Emitting>();
    }

    private void Start()
    {
        steam.StopEmit();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out FireHealth fire))
        {
            if(fire.Damageable.CurrentHealth > Damageable.CurrentHealth)
            {
                
            }
        }
    }

    public void StartExtinguishing(ExtinguishingSubstance substance)
    {
        receivingDamage += substance.Damage;
        steam.StartEmit();
        StartCoroutine(ToFadeOut());
    }

    public void StopExtinguishing(ExtinguishingSubstance substance)
    {
        receivingDamage -= substance.Damage;
        steam.StopEmit();
    }

    public void Die()
    {
        steam.transform.SetParent(null);
        steam.transform.localScale = Vector3.one;
        steam.Die();
        Destroy(fire.gameObject);
    }

    private IEnumerator ToFadeOut()
    {
        while(receivingDamage > 0)
        {
            Damageable.TakeDamage(receivingDamage);
            yield return wait;
        }
    }
}
