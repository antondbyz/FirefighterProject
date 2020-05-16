using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Heat), typeof(Burnable))]
public class Fire : MonoBehaviour 
{
    private Heat heat;
    private Burnable burnable;
    private List<Heat> objectsToHeat = new List<Heat>();
    private Coroutine heatingObjectsCoroutine;

    private void Awake() 
    {
        heat = GetComponent<Heat>();
        burnable = GetComponent<Burnable>();    
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
        {
            objectsToHeat.Add(heat);
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Heat heat = other.GetComponent<Heat>();
        if(heat != null)
        {
            objectsToHeat.Remove(heat);
        }
    }

    private void CheckBurning()
    {
        if(burnable.IsBurning)
        {
            if(heatingObjectsCoroutine == null) 
                heatingObjectsCoroutine = StartCoroutine(HeatingEnteredObjects());
        }
        else
        {
            Destroy(gameObject, 2);
        }
    }

    private IEnumerator HeatingEnteredObjects()
    {
        yield return null; // this is necessary so that the coroutine variable is not null
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(burnable.IsBurning)
        {
            for(int i = 0; i < objectsToHeat.Count; i++)
                objectsToHeat[i].CurrentHeat += heat.CurrentHeat / 10;
                
            yield return delay;
        }
    }
}