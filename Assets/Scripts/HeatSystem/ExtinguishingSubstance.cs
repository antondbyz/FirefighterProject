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
    private float currentSubstanceAmount;

    [SerializeField] private float efficiency = 1;
    [SerializeField] private Image substanceAmountFill = null;

    private ParticleSystem particles;
    private PlayerInput input;
    private PlayerAim aim;
    private List<Heat> objectsToExtinguish = new List<Heat>();
    private Coroutine extinguishingCoroutine;

    private void Awake() 
    {
        particles = GetComponent<ParticleSystem>();
        Transform myRoot = transform.root;
        input = myRoot.GetComponent<PlayerInput>();
        aim = myRoot.GetComponent<PlayerAim>();
        CurrentSubstanceAmount = MAX_SUBSTANCE_AMOUNT;
        TurnOff();
    }

    private void OnEnable() => aim.StoppedAiming += TurnOff;

    private void OnDisable() => aim.StoppedAiming -= TurnOff;

    private void Update() 
    {
        if(input.ExtinguishHeld) TurnOn();
        else TurnOff();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Heat heat = other.GetComponent<Heat>();
        if(heat != null)
            objectsToExtinguish.Add(heat);
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Heat heat = other.GetComponent<Heat>();
        if(heat != null)
            objectsToExtinguish.Remove(heat);
    }

    private void TurnOn()
    {
        if(extinguishingCoroutine == null && CurrentSubstanceAmount > 0 && aim.IsAiming)
        {
            particles.Play();
            extinguishingCoroutine = StartCoroutine(ExtinguishingEnteredObjects());
        }
    }

    private void TurnOff()
    {
        if(extinguishingCoroutine != null)
        {
            particles.Stop();
            StopCoroutine(extinguishingCoroutine);
            extinguishingCoroutine = null;
        }
    }

    private IEnumerator ExtinguishingEnteredObjects()
    {
        WaitForSeconds delay = new WaitForSeconds(0.2f);
        while(true)
        {   
            for(int i = 0; i < objectsToExtinguish.Count; i++)
            {
                if(objectsToExtinguish[i].IsExtinguishable)
                    objectsToExtinguish[i].CurrentHeat -= efficiency;
            }
            CurrentSubstanceAmount--;
            yield return delay;
        }   
    }
}