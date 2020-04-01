using System.Collections;
using UnityEngine;

public class ExtinguishingSubstance : MonoBehaviour
{
    public const float EFFICIENCY = 3;
    public event System.Action OnEmittingStarted;
    private System.Action<float> OnExtinguish;
    private PlayerMovement playerMovement;
    private Transform myTransform;
    private ParticleSystem ps;
    private Collider2D coll;

    private void Awake()
    {
        myTransform = GetComponent<Transform>();
        playerMovement = myTransform.parent.GetComponent<PlayerMovement>();
        ps = GetComponent<ParticleSystem>();
        coll = GetComponent<Collider2D>();
        myTransform.parent.GetComponent<GameController>().OnPaused += StopEmit;
        playerMovement.OnMovingStarted += StopEmit;
        playerMovement.OnDirectionChanged += UpdateDirection;
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

    public void UpdateDirection()
    {
        myTransform.rotation = Quaternion.LookRotation(myTransform.forward, playerMovement.CurrentDirection);
    }

    public void StartEmit() 
    {
        ps.Play();
        coll.enabled = true;
        OnEmittingStarted?.Invoke();
    }

    public void StopEmit() 
    {
        ps.Stop();
        coll.enabled = false;
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
}