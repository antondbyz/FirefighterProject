using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Medic : MonoBehaviour 
{
    [SerializeField] private float recoveringTime = 10;
    [SerializeField] private GameObject treatButton = null;   
    [SerializeField] private GameObject treatmentProgress = null;
    [SerializeField] private Image treatmentProgressFill = null;

    private PlayerInput input;
    private List<GameObject> wounded = new List<GameObject>(); 
    private Coroutine treatingCoroutine;

    private void Awake() 
    {
        input = GetComponent<PlayerInput>();
        StopTreating();    
    }

    private void Update() 
    {
        if(input.MedicHeld) StartTreating();    
        else StopTreating();
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("Wounded"))
        {
            wounded.Add(other.gameObject);
            treatButton.SetActive(true);
        }     
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.CompareTag("Wounded")) 
        {
            wounded.Remove(other.gameObject);
            StopTreating();
        }
    } 

    private void StartTreating()
    {
        if(treatingCoroutine == null && wounded.Count > 0)
        {
            treatingCoroutine = StartCoroutine(Treating(wounded[wounded.Count - 1]));
            treatmentProgress.SetActive(true);
        }
    }

    private void StopTreating()
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

    private IEnumerator Treating(GameObject currentWounded)
    {
        float timeDelay = 0.1f;
        float timer = 0;
        WaitForSeconds delay = new WaitForSeconds(timeDelay);
        while(true)
        {
            yield return delay;
            timer += timeDelay;
            treatmentProgressFill.fillAmount = timer / recoveringTime;
            if(timer >= recoveringTime)
            { 
                Destroy(currentWounded);
                StopTreating();
            }
        }
    }
}