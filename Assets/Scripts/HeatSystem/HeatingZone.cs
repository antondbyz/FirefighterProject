using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingZone : MonoBehaviour 
{
    private List<Heatable> objectsToHeat = new List<Heatable>();
    private Coroutine heatingCoroutine;
    private float currentHeatInfluence;

    public void StartHeatingEnteredObjects(float newHeatInfluence)
    {
        currentHeatInfluence = newHeatInfluence;
        if(heatingCoroutine == null)
            heatingCoroutine = StartCoroutine(HeatingEnteredObjects());
    }

    public void StopHeatingEnteredObjects()
    {
        if(heatingCoroutine != null)
        {
            StopCoroutine(heatingCoroutine);
            heatingCoroutine = null;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Heatable heatable = other.GetComponent<Heatable>();
        if(heatable != null)
            objectsToHeat.Add(heatable);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Heatable heatable = other.GetComponent<Heatable>();
        if(heatable != null)
            objectsToHeat.Remove(heatable);
    }

    private IEnumerator HeatingEnteredObjects()
    {
        float timeDelay = 0.3f;
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        while(true)
        {
            yield return delay;
            for(int i = 0; i < objectsToHeat.Count; i++)
                objectsToHeat[i].CurrentHeat += currentHeatInfluence * timeDelay;
        }
    }    
}