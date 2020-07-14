using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Heat))]
public class Fire : MonoBehaviour
{
    private static Vector2 minScale = new Vector2(0.1f, 0.1f);
    private static Vector2 maxScale = new Vector2(1, 1);

    private Heat heat;
    private ParticleSystem ps;
    private List<Heat> objectsToHeat = new List<Heat>();

    private void Awake()
    {
        heat = GetComponent<Heat>();
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(HeatingEnteredObjects());
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

    private void OnEnable() => heat.HeatChanged += UpdateState;
    
    private void OnDisable() => heat.HeatChanged -= UpdateState;

    private void UpdateState()
    {
        if(heat.CurrentHeat > 0)
        {
            ps.Play();
            transform.localScale = Vector2.Lerp(minScale, maxScale, heat.CurrentHeat / heat.MaxHeat);
        }
        else
        {
            ps.Stop();
            Destroy(gameObject, 2);
        }
    }

    private IEnumerator HeatingEnteredObjects()
    {
        float timeDelay = 0.2f;
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        while(true)
        {
            yield return delay;
            for(int i = 0; i < objectsToHeat.Count; i++)
            {
                if(objectsToHeat[i].IsHeatable)
                    objectsToHeat[i].CurrentHeat += heat.CurrentHeat * timeDelay;
            }
        }
    } 
}