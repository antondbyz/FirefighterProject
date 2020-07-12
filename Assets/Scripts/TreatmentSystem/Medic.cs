using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medic : MonoBehaviour 
{
    [SerializeField] private GameObject treatButton = null;   
    [SerializeField] private GameObject treatmentProgress = null;
    [SerializeField] private Image treatmentProgressFill = null;

    private List<Wounded> wounded = new List<Wounded>(); 
    private Coroutine treatingCoroutine;

    public void StartTreating()
    {
        if(wounded.Count > 0)
        {
            treatingCoroutine = StartCoroutine(Treating(wounded[wounded.Count - 1]));
            treatmentProgress.SetActive(true);
        }
    }

    public void StopTreating()
    {
        treatmentProgress.SetActive(false);
        treatmentProgressFill.fillAmount = 0;
        if(treatingCoroutine != null)
        {
            StopCoroutine(treatingCoroutine);
            treatingCoroutine = null;
        }
        if(wounded.Count == 0) treatButton.SetActive(false); 
    }

    private void Awake() 
    {
        StopTreating();    
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Wounded enteredWounded = other.GetComponent<Wounded>();
        if(enteredWounded != null)
        {
            wounded.Add(enteredWounded);
            treatButton.SetActive(true);
        }     
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Wounded exitedWounded = other.GetComponent<Wounded>();
        if(wounded.Remove(exitedWounded)) 
            StopTreating();
    } 

    private IEnumerator Treating(Wounded currentWounded)
    {
        float timeDelay = 0.1f;
        float timer = 0;
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        while(true)
        {
            yield return delay;
            timer += timeDelay;
            treatmentProgressFill.fillAmount = timer / currentWounded.TimeToRecover;
            if(timer >= currentWounded.TimeToRecover)
            { 
                currentWounded.Recover();
                StopTreating();
            }
        }
    }
}