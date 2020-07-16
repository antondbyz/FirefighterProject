using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Extinguisher : MonoBehaviour
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
    private List<Heat> objectsToExtinguish = new List<Heat>();
    private Coroutine extinguishingCoroutine;

    public void TurnOn()
    {
        if(CurrentSubstanceAmount > 0 && gameObject.activeSelf)
        {
            particles.Play();
            if(extinguishingCoroutine == null)
                extinguishingCoroutine = StartCoroutine(ExtinguishingEnteredObjects());
        }
    }

    public void TurnOff()
    {
        if(gameObject.activeSelf)
        {
            particles.Stop();
            if(extinguishingCoroutine != null)
            {
                StopCoroutine(extinguishingCoroutine);
                extinguishingCoroutine = null;
            }
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
        if(heat != null)
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