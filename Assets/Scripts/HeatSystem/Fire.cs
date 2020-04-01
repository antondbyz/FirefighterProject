using System.Collections;
using UnityEngine;

public class Fire : Heat
{
    [SerializeField] private SparksPool sparksPool = null;
    private System.Action<float> OnBurn;
    private Transform myTransform;
    private ParticleSystem ps;
    private Coroutine sparkingCorouine, burningCoroutine;
    private static Vector2 MinScale = new Vector2(0.1f, 0.1f);

    private void Awake() 
    { 
        myTransform = GetComponent<Transform>();
        ps = GetComponent<ParticleSystem>();
        OnHeatChanged += UpdateState;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out HeatController heatController))
            OnBurn += heatController.ToHeat;
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent(out HeatController heatController))
            OnBurn -= heatController.ToHeat;
    }

    private void UpdateState()
    {
        if(CurrentHeat > 0)
        {
            if(sparkingCorouine == null) sparkingCorouine = StartCoroutine(Sparking());
            if(burningCoroutine == null) burningCoroutine = StartCoroutine(Burning());
            if(!ps.isPlaying) ps.Play();
            myTransform.localScale = Vector2.Lerp(MinScale, Vector2.one, CurrentHeat / MAX_HEAT);
        } 
        else
        {
            if(sparkingCorouine != null) StopCoroutine(sparkingCorouine);
            if(burningCoroutine != null) StopCoroutine(burningCoroutine);
            if(ps.isPlaying) ps.Stop();
            Destroy(gameObject, 2);
        }
    }

    private IEnumerator Burning()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        while(true)
        {
            yield return delay;
            OnBurn?.Invoke(CurrentHeat);
            CurrentHeat++;
        }
    }

    private IEnumerator Sparking()
    {
        WaitForSeconds delay = new WaitForSeconds(5);
        while(true)
        {
            yield return delay;
            int side = Random.Range(0, 2) == 1 ? 1 : -1;
            sparksPool.ThrowSpark(new Vector2(side * 100, 200), CurrentHeat / 10);
        }
    }
}