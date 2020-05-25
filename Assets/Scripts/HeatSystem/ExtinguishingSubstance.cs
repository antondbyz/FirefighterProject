using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishingSubstance : MonoBehaviour
{
    [SerializeField] private float efficiency = 1;

    private ParticleSystem ps;
    private List<Heat> objectsToExtinguish = new List<Heat>();
    private Coroutine extinguishingCoroutine;

    public void Play()
    {
        ps.Play();
        if(extinguishingCoroutine == null)
            extinguishingCoroutine = StartCoroutine(Extinguishing());
    }

    public void Stop()
    {
        ps.Stop();
        if(extinguishingCoroutine != null)
        {
            StopCoroutine(extinguishingCoroutine);
            extinguishingCoroutine = null;
        }
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
        Heat heat = other.GetComponent<Heat>();
        if(heat != null && heat.IsExtinguishable)
            objectsToExtinguish.Add(heat);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Heat heat = other.GetComponent<Heat>();
        if(heat != null)
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
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(true)
        {   
            yield return delay;
            for(int i = 0; i < objectsToExtinguish.Count; i++)
                objectsToExtinguish[i].CurrentHeat -= efficiency;
        }   
    }
}