using System.Collections;
using UnityEngine;

public class Burnable : HeatController
{
    public override float CurrentHeat
    {
        get { return currentHeat; }
        set
        {
            if(value >= 0)
            {
                float oldHeat = currentHeat;
                currentHeat = value;
                OnHeatChanged.Invoke();
                OnHeatChangedDelta.Invoke(currentHeat - oldHeat);
                if(burningEffect != null) UpdateBurning();
            }
        }   
    }
    public FloatEvent OnHeatChangedDelta { get; } = new FloatEvent();
    [SerializeField] private Fire burningEffect = null;
    private Coroutine heatingCoroutine;

    private void UpdateBurning()
    {
        if(burningEffect.IsBurning)
        {
            if(heatingCoroutine == null) heatingCoroutine = StartCoroutine(ToHeat());
        }
        else
        {
            if(heatingCoroutine != null)
            {
                StopCoroutine(heatingCoroutine);
                heatingCoroutine = null;
            }
        }
    }

    private IEnumerator ToHeat()
    {
        WaitForSeconds delay = new WaitForSeconds(1);
        while(true)
        {
            yield return delay;
            TargetHeat++;
        }
    }
}