using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ExtinguishingSubstance : MonoBehaviour
{
    public const float MAX_SUBSTANCE_AMOUNT = 100;

    public float CurrentSubstanceAmount
    {
        get => currentSubstanceAmount;
        set
        {
            if(value > MAX_SUBSTANCE_AMOUNT) value = MAX_SUBSTANCE_AMOUNT;
            else if(value <= 0)
            {
                value = 0;
                TurnOff();
            }
            currentSubstanceAmount = value;
            substanceAmountFill.fillAmount = currentSubstanceAmount / MAX_SUBSTANCE_AMOUNT;
        }
    }
    public bool IsTurnedOn { get; private set; }

    [SerializeField] private float efficiency = 1;
    [SerializeField] private float substanceDecreasingSpeed = 0.1f;
    [SerializeField] private Image substanceAmountFill = null;

    private ParticleSystem particles;
    private PlayerInput input;
    private PlayerAim aim;
    private List<Fire> enteredFires = new List<Fire>();
    private float currentSubstanceAmount;

    private void Awake() 
    {
        particles = GetComponent<ParticleSystem>();
        Transform playerCharacter = transform.root.GetChild(0);
        input = playerCharacter.GetComponent<PlayerInput>();
        aim = playerCharacter.GetComponent<PlayerAim>();
        CurrentSubstanceAmount = MAX_SUBSTANCE_AMOUNT;
        TurnOff();
    }

    private void OnEnable() 
    { 
        StartCoroutine(Extinguishing());
        aim.StoppedAiming += TurnOff;
    }

    private void OnDisable() => aim.StoppedAiming -= TurnOff;

    private void Update() 
    {
        if(input.ExtinguishHeld) TurnOn();
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
        if(!IsTurnedOn && CurrentSubstanceAmount > 0 && aim.IsAiming)
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
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while(true)
        {   
            yield return delay;
            if(IsTurnedOn)
            {
                for(int i = 0; i < enteredFires.Count; i++) enteredFires[i].CurrentHeat -= efficiency;
                CurrentSubstanceAmount -= substanceDecreasingSpeed;
            }
        }   
    }
}