using System.Collections;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ExtinguishingSubstance : Emitting
{
    public float Damage => damage;

    [Header("Damage that takes fire")] [SerializeField] private float damage = 0.5f;
    [Header("Steam effect that appears at substance's collision point")] [SerializeField] private Emitting steam;

    private PolygonCollider2D polyCollider;
    private CollisionModule particlesCollision;
    private Coroutine collisionsCoroutine;
    private WaitForSeconds delay = new WaitForSeconds(0.3f);
    private RaycastHit2D hit;

    protected override void Awake()
    {
        emission = GetComponent<ParticleSystem>().emission;
        polyCollider = GetComponent<PolygonCollider2D>();
        polyCollider.enabled = false;
        particlesCollision = GetComponent<ParticleSystem>().collision;
    }

    public override void StartEmit()
    {
        base.StartEmit();
        collisionsCoroutine = StartCoroutine(CheckCollisions());
    }

    public override void StopEmit()
    {
        base.StopEmit();
        polyCollider.enabled = false;
        if(collisionsCoroutine != null) StopCoroutine(collisionsCoroutine);
        steam.StopEmit();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out FireHealth fire))
        {
            fire.StartExtinguishing(this);
        }
        else if(other.gameObject.TryGetComponent(out Spark spark))
        {
            Destroy(spark.gameObject);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.gameObject.TryGetComponent(out FireHealth fire))
        {
            fire.StopExtinguishing(this);
        }
    }

    private IEnumerator CheckCollisions()
    {
        yield return delay;
        polyCollider.enabled = true;
        for(;;)
        {
            int layerMask = 1 << GameManager.Layers.IGNORE_EXTINGUISHING_SUBSTANCE;
            hit = Physics2D.Raycast(transform.position, transform.up, 2.5f, ~layerMask);
            if(hit.collider != null)
            {
                if(!hit.collider.TryGetComponent(out FireHealth fire))
                {
                    steam.transform.position = hit.point;
                    steam.StartEmit();
                    particlesCollision.enabled = true;
                }
                else
                {
                    steam.StopEmit();
                    particlesCollision.enabled = false;
                }
            }
            else
            {
                steam.StopEmit();
                particlesCollision.enabled = false;
            }
            yield return delay;
        }
    }
}