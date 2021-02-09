using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ElectricShot : MonoBehaviour
{
    [Tooltip("Collider active time relative to the particle lifetime")]
    [Range(0, 1)] [SerializeField] private float colliderActiveTime = 1;

    private IEnumerator Start() 
    {
        ParticleSystem myParticles = GetComponent<ParticleSystem>();
        BoxCollider2D myCollider = GetComponent<BoxCollider2D>();    
        MainModule main = myParticles.main;
        float colliderDuration = main.startLifetime.constant * colliderActiveTime;
        WaitForSeconds disableColliderDelay = new WaitForSeconds(colliderDuration);
        WaitForSeconds playEffectDelay = new WaitForSeconds(main.duration - colliderDuration);
        while(true)
        {
            myParticles.Play();
            myCollider.enabled = true;
            yield return disableColliderDelay;
            myCollider.enabled = false;
            yield return playEffectDelay;
        }
    }
}