using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Extinguisher : MonoBehaviour
{
    public const float MAX_SUBSTANCE_AMOUNT = 100;

    public event System.Action TurnedOn;
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
    private List<Heat> objectsToExtinguish = new List<Heat>();
    private Coroutine extinguishingCoroutine;
    private Coroutine decreasingSubstanceCoroutine;

    public void TurnOn()
    {
        if(CurrentSubstanceAmount > 0)
        {
            particles.Play();
            if(extinguishingCoroutine == null)
                extinguishingCoroutine = StartCoroutine(Extinguishing());
            if(decreasingSubstanceCoroutine == null)
                decreasingSubstanceCoroutine = StartCoroutine(DecreasingSubstanceAmount());
            TurnedOn?.Invoke();
        }
    }

    public void TurnOff()
    {
        particles.Stop();
        if(extinguishingCoroutine != null)
        {
            StopCoroutine(extinguishingCoroutine);
            extinguishingCoroutine = null;
        }
        if(decreasingSubstanceCoroutine != null)
        {
            StopCoroutine(decreasingSubstanceCoroutine);
            decreasingSubstanceCoroutine = null;
        }
    }

    private void Awake() 
    {
        particles = GetComponent<ParticleSystem>();
        CurrentSubstanceAmount = MAX_SUBSTANCE_AMOUNT;
        TurnOff();
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
        if(Input.GetKeyDown(KeyCode.E)) TurnOn();
        else if(Input.GetKeyUp(KeyCode.E)) TurnOff();
    }
#endif

    private IEnumerator Extinguishing()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(true)
        {   
            for(int i = 0; i < objectsToExtinguish.Count; i++)
                objectsToExtinguish[i].CurrentHeat -= efficiency;
            yield return delay;
        }   
    }

    private IEnumerator DecreasingSubstanceAmount()
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while(true)
        {
            CurrentSubstanceAmount--;
            yield return delay;
        }
    }
}