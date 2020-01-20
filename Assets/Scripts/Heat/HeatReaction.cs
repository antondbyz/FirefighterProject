using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HeatController))]
public class HeatReaction : MonoBehaviour
{
    [SerializeField] private new SpriteRenderer renderer = null;
    [SerializeField] private Health health = null;
    private HeatController heatController;
    private Color normalColor;
    private Coroutine damageCoroutine;

    private void Awake() 
    {
        heatController = GetComponent<HeatController>();   
        if(renderer != null) 
        {
            normalColor = renderer.color;
            heatController.OnHeatChanged.AddListener(UpdateColor);
        }
        if(health != null)
        { 
            heatController.OnHeatChanged.AddListener(UpdateTakingDamage);
        } 
    }

    private void UpdateColor()
    {
        renderer.color = Color.Lerp(normalColor, Color.red, heatController.CurrentHeat / 100);
    }

    private void UpdateTakingDamage()
    {
        if(damageCoroutine != null) StopCoroutine(damageCoroutine);
        damageCoroutine = StartCoroutine(TakingDamage(heatController.CurrentHeat * 0.01f));
    }

    private IEnumerator TakingDamage(float damage)
    {
        WaitForSeconds delay = new WaitForSeconds(0.1f);
        while(true)
        {
            health.CurrentHealth -= damage;
            yield return delay;
        }
    }
}