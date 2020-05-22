using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishingSubstance : MonoBehaviour
{
    [SerializeField] private float efficiency = 1;
    private List<Heat> objectsToExtinguish = new List<Heat>();
    private ParticleSystem ps;
    private Coroutine extinguishingCoroutine;

    public void Play()
    {
        if(extinguishingCoroutine == null)
            extinguishingCoroutine = StartCoroutine(Extinguishing());
        
        ps.Play();
    }

    public void Stop()
    {
        if(extinguishingCoroutine != null)
        {
            StopCoroutine(extinguishingCoroutine);
            extinguishingCoroutine = null;
        }
        ps.Stop();
    }

    private void Awake() 
    {
        ps = GetComponent<ParticleSystem>();  
        Stop();
    }

    private void OnEnable() 
    {
        PauseManager.OnPaused += Stop;
    }

    private void OnDisable() 
    {
        PauseManager.OnPaused -= Stop;   
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Heat heat)) 
            objectsToExtinguish.Add(heat);   
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Heat heat)) 
            objectsToExtinguish.Remove(heat);    
    }

#if UNITY_EDITOR
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) Play();
        else if(Input.GetKeyUp(KeyCode.E)) Stop();
    }
#endif

    private IEnumerator Extinguishing()
    {
        yield return null; // this is necessary so that the coroutine variable is not null
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(true)
        {   
            for(int i = 0; i < objectsToExtinguish.Count; i++)
                objectsToExtinguish[i].CurrentHeat -= efficiency;
            yield return delay;
        }   
    }
}