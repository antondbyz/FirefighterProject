using System.Collections;
using UnityEngine;

public class Burnable : Damageable
{
    public virtual float ReceivingDamage
    {
        get { return receivingDamage; }
        protected set
        {
            if(burningEffect && !burningEffect.isPlaying) burningEffect.Play();
            else if(receivingDamage > 0 && value <= 0)
            {
                if(burningEffect) burningEffect.Stop();
                receivingDamage = 0;
                return;
            }
            receivingDamage = value;
        }
    }

    [SerializeField] protected ParticleSystem burningEffect = null;
    protected float receivingDamage;
    protected Coroutine burningCoroutine;
    protected WaitForSeconds delay = new WaitForSeconds(0.1f);

    public virtual void StartBurning(Fire fire)
    {
        fire.OnDamageChanged.AddListener(FireDamageChanged);
        ReceivingDamage += fire.Damage; 
        if(burningCoroutine == null) burningCoroutine = StartCoroutine(ToBurn());
    }

    public virtual void StopBurning(Fire fire)
    {
        fire.OnDamageChanged.RemoveListener(FireDamageChanged);
        ReceivingDamage -= fire.Damage / 2;
    }

    protected virtual void FireDamageChanged(float value)
    {
        if(value >= 0) ReceivingDamage += value;
        else receivingDamage += value / 2;
    }

    protected virtual IEnumerator ToBurn()
    {
        while(receivingDamage > 0)
        {
            TakeDamage(receivingDamage);
            yield return delay;
        }
    }
}