using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeatingZone : MonoBehaviour
{
    public float HeatingSpeed 
    {
        get => heatingSpeed;
        set { heatingSpeed = value; }
    }

    [SerializeField] private float heatingSpeed = 0;

    private List<Heat> objectsToHeat = new List<Heat>();

    private void Start() 
    {
        StartCoroutine(HeatingEnteredObjects());
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Heat heat = other.GetComponent<Heat>();
        if(heat != null && heat.IsHeatable)
            objectsToHeat.Add(heat);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Heat heat = other.GetComponent<Heat>();
        if(heat != null)
            objectsToHeat.Remove(heat);
    }

    private IEnumerator HeatingEnteredObjects()
    {
        float timeDelay = 0.3f;
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        while(true)
        {
            yield return delay;
            for(int i = 0; i < objectsToHeat.Count; i++)
                objectsToHeat[i].CurrentHeat += heatingSpeed * timeDelay;
        }
    }    
}