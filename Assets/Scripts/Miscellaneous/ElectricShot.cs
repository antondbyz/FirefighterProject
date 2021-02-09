using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ElectricShot : MonoBehaviour
{
    [Tooltip("Collider active time relative to the particle lifetime")]
    [Range(0, 1)] [SerializeField] private float lifetimeMultiplier = 1;

    private IEnumerator Start() 
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();    
        MainModule main = ps.main;
        float colliderLifetime = main.startLifetime.constant * lifetimeMultiplier;
        WaitForSeconds disableColliderDelay = new WaitForSeconds(colliderLifetime);
        WaitForSeconds playEffectDelay = new WaitForSeconds(main.duration - colliderLifetime);
        while(true)
        {
            ps.Play();
            myCollider.enabled = true;
            yield return disableColliderDelay;
            myCollider.enabled = false;
            yield return playEffectDelay;
        }
    }
}