using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ExtinguishingSubstance : MonoBehaviour
{
    public float Damage => damage;
    [SerializeField] private LayerMask raycastLayerMask = new LayerMask();
    [Header("Damage that takes fire")] [SerializeField] private float damage = 0.5f;
    [Header("Steam effect that appears at substance's collision point")] [SerializeField] private ParticleSystem steamEffect = null;

    private ParticleSystem substanceEffect;
    private PolygonCollider2D polyCollider;
    private CollisionModule particlesCollision;
    private Coroutine checkCollisions;
    private WaitForSeconds delay = new WaitForSeconds(0.3f);
    private RaycastHit2D hit;

    private void Awake()
    {
        substanceEffect = GetComponent<ParticleSystem>();
        polyCollider = GetComponent<PolygonCollider2D>();
        polyCollider.enabled = false;
        particlesCollision = GetComponent<ParticleSystem>().collision;
    }

    public void StartEmit()
    {
        substanceEffect.Play();
        checkCollisions = StartCoroutine(CheckCollisions());
    }

    public void StopEmit()
    {
        substanceEffect.Stop();
        polyCollider.enabled = false;
        if(checkCollisions != null) StopCoroutine(checkCollisions);
        steamEffect.Stop();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.TryGetComponent(out FireHealth fire))
        {
            fire.StartExtinguishing(this);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.TryGetComponent(out FireHealth fire))
        {
            fire.StopExtinguishing(this);
        }
    }

    private IEnumerator CheckCollisions()
    {
        while(true)
        {
            yield return delay;
            if(!polyCollider.enabled) polyCollider.enabled = true;
            hit = Physics2D.Raycast(transform.position, transform.up, 2.5f, raycastLayerMask);
            if(hit.collider != null)
            {
                steamEffect.transform.position = hit.point;
                if(!particlesCollision.enabled) particlesCollision.enabled = true;

                // Delay is needed to ensure that the steam has moved to the desired point
                if(!steamEffect.isEmitting) StartCoroutine(steamEffect.Play(0.1f));
            }
            else
            {
                if(steamEffect.isEmitting) steamEffect.Stop();
                if(particlesCollision.enabled) particlesCollision.enabled = false;
            }
        }
    }
}