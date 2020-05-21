using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Heat))]
public class Fire : MonoBehaviour 
{
    public bool IsBurning => heat.CurrentHeat > 0;

    private Heat heat;
    private ParticleSystem ps;
    private List<Heat> objectsToHeat = new List<Heat>();
    private Coroutine heatingObjectsCoroutine;
    private IScalable scalable;

    private void Awake() 
    {
        heat = GetComponent<Heat>(); 
        ps = GetComponent<ParticleSystem>();
        scalable = GetComponent<IScalable>(); 
    }

    private void OnEnable() 
    {
        heat.HeatChanged += CheckBurning;
    }

    private void OnDisable() 
    {
        heat.HeatChanged -= CheckBurning;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Heat heat = other.GetComponent<Heat>();
        if(heat != null)
            objectsToHeat.Add(heat);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Heat heat = other.GetComponent<Heat>();
        if(heat != null)
            objectsToHeat.Remove(heat);
    }

    private void CheckBurning()
    {
        if(IsBurning)
        {
            scalable?.LerpScale(heat.CurrentHeat / heat.MaxHeat);
            ps.Play();
            if(heatingObjectsCoroutine == null) 
                heatingObjectsCoroutine = StartCoroutine(HeatingEnteredObjects());
        }
        else
        {
            ps.Stop();
            Destroy(gameObject, 2);
        }
    }

    private IEnumerator HeatingEnteredObjects()
    {
        yield return null; // this is necessary so that the coroutine variable is not null
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(IsBurning)
        {
            for(int i = 0; i < objectsToHeat.Count; i++)
                objectsToHeat[i].CurrentHeat += heat.CurrentHeat / 10;
                
            yield return delay;
        }
    }
}