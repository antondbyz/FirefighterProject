using System.Collections;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    public const float EFFICIENCY = 3;
    private System.Action<float> OnExtinguish;
    private Transform myTransform;
    private ParticleSystem ps;
    private Collider2D coll;
    private PlayerMovement playerMovement;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        ps = GetComponent<ParticleSystem>();
        coll = GetComponent<Collider2D>();
        playerMovement = myTransform.parent.GetComponent<PlayerMovement>();
        playerMovement.DirectionChanged += UpdateDirection;
        PauseManager.OnPaused += StopEmit;
    }

    private void Start() 
    {
        StopEmit();
        StartCoroutine(Extinguishing());
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) StartEmit();
        else if(Input.GetKeyUp(KeyCode.E)) StopEmit();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Heat heat)) OnExtinguish += heat.ToHeat;   
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Heat heat)) OnExtinguish -= heat.ToHeat;    
    }

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
    
    private void UpdateDirection()
    {
        myTransform.rotation = Quaternion.LookRotation(myTransform.forward, playerMovement.CurrentDirection);
    }

    private IEnumerator Extinguishing()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(true)
        {   
            yield return delay;
            OnExtinguish?.Invoke(-EFFICIENCY);
        }
    }

    private void OnDisable() 
    {
        playerMovement.DirectionChanged -= UpdateDirection; 
        PauseManager.OnPaused -= StopEmit;   
    }
}