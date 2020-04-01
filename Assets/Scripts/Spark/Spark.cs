using System.Collections;
using UnityEngine;

public class Spark : MonoBehaviour
{
    [SerializeField] private Fire firePrefab = null;
    private Rigidbody2D rb;
    private Collider2D coll;
    private ParticleSystem ps;
    private WaitForSeconds lifetime = new WaitForSeconds(3);
    private int groundLayer;
    private float heatInfluence;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        coll = GetComponent<Collider2D>();
        ps = GetComponent<ParticleSystem>();
        groundLayer = LayerMask.NameToLayer("Ground");
    }

    private void SetActive(bool value)
    {
        rb.simulated = value;
        coll.enabled = value;
        if(value) ps.Play();
        else ps.Stop();
    }

    public IEnumerator Throw(Vector2 force, float heat)
    {
        SetActive(true);
        heatInfluence = heat;
        rb.velocity = Vector2.zero;
        rb.AddForce(force);
        yield return lifetime;
        SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        other.GetComponent<Heat>()?.ToHeat(heatInfluence);
        if(other.gameObject.layer == groundLayer)
        {
            Fire newFire = Instantiate(firePrefab, transform.position, Quaternion.identity);
            newFire.ToHeat(heatInfluence);
            SetActive(false);
        }
        else if(!other.isTrigger || other.GetComponent<Fire>() || other.GetComponent<ExtinguishingSubstance>())
            SetActive(false);
    }
}