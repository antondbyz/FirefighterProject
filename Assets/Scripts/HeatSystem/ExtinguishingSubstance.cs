using System.Collections;
using UnityEngine;

public class ExtinguishingSubstance : MonoBehaviour
{
    public event System.Action<float> Extinguish;
    public bool IsExtinguishing { get; private set; }

    [SerializeField] private float efficiency = 1;
    private ParticleSystem ps;

    public void Play()
    {
        ps.Play();
        IsExtinguishing = true;
    }

    public void Stop()
    {
        ps.Stop();
        IsExtinguishing = false;
    }

    private void Awake() 
    {
        ps = GetComponent<ParticleSystem>();  
    }

    private void Start() 
    {
        StartCoroutine(Extinguishing());    
    }

    private void OnEnable() 
    {
        PauseManager.OnPaused += Stop;
    }

    private void OnDisable() 
    {
        PauseManager.OnPaused -= Stop;   
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
            if(IsExtinguishing) Extinguish?.Invoke(efficiency);
        }   
    }
}