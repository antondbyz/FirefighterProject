using System.Collections;
using UnityEngine;

public class Burnable : MonoBehaviour
{
    public float ReceivingDamage
    {
        get { return receivingDamage; }
        private set
        {
            if(receivingDamage == 0 && value > 0) burningEffect.StartEmit();
            else if(receivingDamage > 0 && value <= 0)
            {
                burningEffect.StopEmit();
                receivingDamage = 0;
                return;
            }
            receivingDamage = value;
        }
    }

    [SerializeField] private Emitting burningEffect = null;
    private float receivingDamage;
    private Damageable damageable;
    private Coroutine burningCoroutine;
    private WaitForSeconds delay = new WaitForSeconds(0.1f);

    private void Awake()
    {
        damageable = GetComponent<Damageable>();
    }

    private void Start()
    {
        burningEffect.StopEmit();
    }

    public void StartBurning(FireDamage fire)
    {
        fire.OnDamageChanged.AddListener(FireDamageChanged);
        ReceivingDamage += fire.Damage; 
        if(burningCoroutine == null) burningCoroutine = StartCoroutine(ToBurn());
    }

    public void StopBurning(FireDamage fire)
    {
        fire.OnDamageChanged.RemoveListener(FireDamageChanged);
        ReceivingDamage -= fire.Damage / 2;
    }

    private void FireDamageChanged(float value)
    {
        if(value >= 0) ReceivingDamage += value;
        else receivingDamage += value / 2;
    }

    private IEnumerator ToBurn()
    {
        while(receivingDamage > 0)
        {
            damageable.TakeDamage(receivingDamage);
            yield return delay;
        }
    }
}