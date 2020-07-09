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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Wounded enteredWounded = other.GetComponent<Wounded>();
        if(enteredWounded != null && enteredWounded.CurrentHealth < enteredWounded.MaxHealth)
        {
            wounded.Add(enteredWounded);
            treatButton.SetActive(true);
        }     
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        Wounded exitedWounded = other.GetComponent<Wounded>();
        if(wounded.Remove(exitedWounded)) 
        {
            treatmentProgress.SetActive(false);
            if(wounded.Count == 0) treatButton.SetActive(false); 
        }
    } 

    public void StartTreating()
    {
        if(wounded.Count > 0)
        {
            if(treatingCoroutine != null) StopTreating();
            treatingCoroutine = StartCoroutine(Treating(wounded[wounded.Count - 1]));
            treatmentProgress.SetActive(true);
        }
    }

    public void StopTreating()
    {
        StopCoroutine(treatingCoroutine);
        treatingCoroutine = null;
        treatmentProgress.SetActive(false);
        if(wounded.Count == 0) treatButton.SetActive(false); 
    }

    private IEnumerator Treating(Wounded currentWounded)
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while(true)
        {
            currentWounded.CurrentHealth++;
            treatmentProgressFill.fillAmount = currentWounded.CurrentHealth / currentWounded.MaxHealth;
            if(currentWounded.CurrentHealth >= currentWounded.MaxHealth) 
            {
                wounded.Remove(currentWounded);
                if(wounded.Count == 0) StopTreating();
                else currentWounded = wounded[wounded.Count - 1];
            }
            yield return delay;
        }
    }
}