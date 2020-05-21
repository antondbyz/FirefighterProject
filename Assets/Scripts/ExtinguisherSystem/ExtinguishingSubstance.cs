using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishingSubstance : MonoBehaviour
{
    [SerializeField] private float efficiency = 1;
    private List<Heat> objectsToExtinguish = new List<Heat>();
    private ParticleSystem ps;
    private Collider2D coll;

    public void StartEmit() 
    {
        ps.Play();
        coll.enabled = true;
    }

    public void StopEmit() 
    {
        ps.Stop();
        coll.enabled = false;
    }

    private void Awake() 
    {
        ps = GetComponent<ParticleSystem>();    
        coll = GetComponent<Collider2D>();
    }

    private void Start() 
    {
        StopEmit();
        StartCoroutine(Extinguishing());
    }

    private void OnEnable() 
    {
        PauseManager.OnPaused += StopEmit;
    }

    private void OnDisable() 
    {
        PauseManager.OnPaused -= StopEmit;   
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
        if(Input.GetKeyDown(KeyCode.E)) StartEmit();
        else if(Input.GetKeyUp(KeyCode.E)) StopEmit();
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