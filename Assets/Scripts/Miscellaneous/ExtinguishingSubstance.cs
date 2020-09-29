using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtinguishingSubstance : MonoBehaviour
{
    public float MaxSubstanceAmount => maxSubstanceAmount;

    public float CurrentSubstanceAmount
    {
        get => currentSubstanceAmount;
        private set
        {
            value = Mathf.Clamp(value, 0, maxSubstanceAmount);
            currentSubstanceAmount = value;
            substanceAmountFill.fillAmount = currentSubstanceAmount / maxSubstanceAmount;
        }
    }
    public bool IsTurnedOn { get; private set; }

    [SerializeField] private float efficiency = 1;
    [SerializeField] private float maxSubstanceAmount = 100;
    [SerializeField] private Image substanceAmountFill = null;

    private ParticleSystem particles;
    private PlayerInput input;
    private PlayerAim aim;
    private List<Fire> enteredFires = new List<Fire>();
    private float currentSubstanceAmount;

    public void Refill() => CurrentSubstanceAmount = maxSubstanceAmount;

    private void Awake() 
    {
        particles = GetComponent<ParticleSystem>();
        Transform parent = transform.parent;
        input = parent.GetComponent<PlayerInput>();
        aim = parent.GetComponent<PlayerAim>();
        CurrentSubstanceAmount = maxSubstanceAmount;
        TurnOff();
    }

    private void OnEnable() => StartCoroutine(Extinguishing());

    private void Update() 
    {
        if(input.ExtinguishHeld && CurrentSubstanceAmount > 0 && aim.IsAiming) TurnOn();
        else TurnOff();    
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

    private void TurnOn()
    {
        if(!IsTurnedOn)
        {
            particles.Play();
            IsTurnedOn = true;
        }
    }

    private void TurnOff()
    {
        if(IsTurnedOn)
        {
            particles.Stop();
            IsTurnedOn = false;
        }
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
                for(int i = 0; i < enteredFires.Count; i++) enteredFires[i].CurrentHeat -= efficiency;
                CurrentSubstanceAmount -= timeDelay;
            }
        }   
    }
}