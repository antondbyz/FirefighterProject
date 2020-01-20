using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ExtinguishingSubstance : MonoBehaviour
{
    [SerializeField] private LayerMask raycastLayerMask = new LayerMask();
    [SerializeField] private float extinguishingSpeed = 2f;
    [SerializeField] private ParticleSystem steamEffect = null;
    private new Collider2D collider;
    private ParticleSystem substanceEffect;
    private CollisionModule particlesCollision;
    private HeatController extinguishingTarget;

    private void Awake()
    {
        collider = GetComponent<Collider2D>();
        substanceEffect = GetComponent<ParticleSystem>();
        particlesCollision = substanceEffect.collision;
        StartCoroutine(CheckCollisions());
        StartCoroutine(ToExtinguish());
        StopEmit();
    }

    public void StartEmit()
    {
        substanceEffect.Play();
        collider.enabled = true;
    }

    public void StopEmit()
    {
        substanceEffect.Stop();
        steamEffect.Stop();
        collider.enabled = false;
        extinguishingTarget = null;
    }

    private IEnumerator CheckCollisions()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        RaycastHit2D hit;
        while(true)
        {
            yield return delay;
            if(substanceEffect.isEmitting)
            {
                hit = Physics2D.Raycast(transform.position, transform.up, 2.5f, raycastLayerMask);
                if(hit.collider != null)
                {
                    steamEffect.transform.position = hit.point;
                    particlesCollision.enabled = true;
                    if(!steamEffect.isEmitting) steamEffect.Play();
                    if(hit.collider.TryGetComponent(out HeatController heatController))
                        extinguishingTarget = heatController;
                    else extinguishingTarget = null;    
                }
                else
                {
                    if(steamEffect.isEmitting) steamEffect.Stop();
                    particlesCollision.enabled = false;
                    extinguishingTarget = null;
                }
            }
        }
    }

    private IEnumerator ToExtinguish()
    {
        WaitForSeconds delay = new WaitForSeconds(1);
        while(true)
        {
            if(extinguishingTarget != null)
            {
                extinguishingTarget.CurrentHeat -= extinguishingSpeed;
                extinguishingTarget.TargetHeat -= extinguishingSpeed;
            } 
            yield return delay;
        }
    }
}