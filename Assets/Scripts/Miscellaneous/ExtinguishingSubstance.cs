using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExtinguishingSubstance : MonoBehaviour
{
    public bool IsTurnedOn 
    {
        get => isTurnedOn;
        private set
        {
            if(isTurnedOn != value)
            {
                isTurnedOn = value;
                if(isTurnedOn) ps.Play();
                else ps.Stop();
            }
        }
    }

    [SerializeField] private float efficiency = 1;

    private ParticleSystem ps;
    private PlayerAim aim;
    private List<Fire> enteredFires = new List<Fire>();
    private bool isTurnedOn;

    private void Awake() 
    {
        ps = GetComponent<ParticleSystem>();
        aim = transform.parent.GetComponent<PlayerAim>();
    }

    private void OnEnable() => StartCoroutine(Extinguishing());

    private void Update() 
    {
        IsTurnedOn = InputManager.ExtinguishHeld && aim.IsAiming;  
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Fire fire = other.GetComponent<Fire>();
        if(fire != null) enteredFires.Add(fire);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Fire fire = other.GetComponent<Fire>();
        if(fire != null) enteredFires.Remove(fire);
    }

    private IEnumerator Extinguishing()
    {
        float timeDelay = 0.1f;
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        while(true)
        {   
            yield return delay;
            if(IsTurnedOn)
            {
                for(int i = 0; i < enteredFires.Count; i++) enteredFires[i].CoolDown(efficiency);
            }
        }   
    }
}