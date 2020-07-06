using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Extinguisher : MonoBehaviour
{
    public event System.Action TurnedOn;
    public float CurrentSubstanceAmount
    {
        get => currentSubstanceAmount;
        set
        {
            currentSubstanceAmount = value;
            substanceAmountFillArea.fillAmount = currentSubstanceAmount / MAX_SUBSTANCE_AMOUNT;
            if(currentSubstanceAmount <= 0) TurnOff();
            else if(currentSubstanceAmount > MAX_SUBSTANCE_AMOUNT) 
                currentSubstanceAmount = MAX_SUBSTANCE_AMOUNT;
        }
    }
    private float currentSubstanceAmount;

    [SerializeField] private float efficiency = 1;
    [SerializeField] private Image substanceAmountFillArea = null;

    private ParticleSystem ps;
    private List<Heat> objectsToExtinguish = new List<Heat>();
    private Coroutine extinguishingCoroutine;
    private Coroutine decreasingSubstanceCoroutine;
    private const float MAX_SUBSTANCE_AMOUNT = 100;

    public void TurnOn()
    {
        if(CurrentSubstanceAmount > 0)
        {
            ps.Play();
            if(extinguishingCoroutine == null)
                extinguishingCoroutine = StartCoroutine(Extinguishing());
            if(decreasingSubstanceCoroutine == null)
                decreasingSubstanceCoroutine = StartCoroutine(DecreasingSubstanceAmount());
            TurnedOn?.Invoke();
        }
    }

    public void TurnOff()
    {
        ps.Stop();
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
        ps = GetComponent<ParticleSystem>();
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

    private void OnEnable() 
    {
        PauseManager.OnPaused += TurnOff;
    }

    private void OnDisable() 
    {
        PauseManager.OnPaused -= TurnOff;   
    }
}