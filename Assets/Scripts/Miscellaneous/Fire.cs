using System.Collections;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public const float MAX_HEAT = 100;

    private static Vector2 maxSize = Vector2.one;
    private static Vector2 minSize = new Vector2(0.1f, 0.1f);

    public float CurrentHeat
    {
        get => currentHeat;
        set
        {
            value = Mathf.Clamp(value, 0, MAX_HEAT);
            currentHeat = value;
            if(currentHeat > 0)
            {
                ps.Play();
                myTransform.localScale = Vector2.Lerp(minSize, maxSize, currentHeat / MAX_HEAT);
            }
            else
            {
                ps.Stop();
                Destroy(myCollider);
                Destroy(gameObject, 2);
            }
        }
    }

    [Range(0, MAX_HEAT)] [SerializeField] private float currentHeat = MAX_HEAT;

    private Transform myTransform;
    private Collider2D myCollider;
    private ParticleSystem ps;
    private Coroutine heatingCoroutine;

    private void Awake()
    {
        myTransform = transform;
        myCollider = GetComponent<Collider2D>();
        ps = GetComponent<ParticleSystem>();
        CurrentHeat = currentHeat;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        PlayerHeat playerHeat = other.GetComponent<PlayerHeat>();
        if(playerHeat != null) heatingCoroutine = StartCoroutine(Heating(playerHeat));
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.GetComponent<PlayerHeat>()) StopCoroutine(heatingCoroutine);
    }

    private IEnumerator Heating(PlayerHeat playerHeat)
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        while(true)
        {
            playerHeat.CurrentHeat += currentHeat;
            yield return delay;
        }
    }
}