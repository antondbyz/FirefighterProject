using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class HeatController : MonoBehaviour
{
    public bool debug;
    public virtual float CurrentHeat
    {
        get { return currentHeat; }
        set
        {
            if(value >= 0) currentHeat = value;
            else currentHeat = 0;
            OnHeatChanged.Invoke();
        }
    }
    public virtual float TargetHeat
    {
        get { return targetHeat; }
        set
        {
            if(targetHeatCoroutine != null) StopCoroutine(targetHeatCoroutine);
            if(value >= 0) targetHeat = value;
            else targetHeat = 0;
            targetHeatCoroutine = StartCoroutine(MoveToTargetHeat());
        }
    }
    public UnityEvent OnHeatChanged { get; protected set; } = new UnityEvent();

    [SerializeField] protected HeatData heatData = null;
    [SerializeField] protected float currentHeat = 0;
    protected float targetHeat;
    private Coroutine targetHeatCoroutine;

    protected virtual void Start() 
    {
        CurrentHeat = currentHeat;
        TargetHeat = currentHeat;
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Fire fire))
        {
            TargetHeat += fire.Burnable.CurrentHeat;
            fire.Burnable.OnHeatChangedDelta.AddListener(OnFireHeatChanged);
        }    
    }

    private void OnTriggerExit2D(Collider2D other) 
    {
        if(other.TryGetComponent(out Fire fire))
        {
            TargetHeat -= fire.Burnable.CurrentHeat;
            fire.Burnable.OnHeatChangedDelta.RemoveListener(OnFireHeatChanged);
        }    
    }

    protected virtual void OnFireHeatChanged(float delta)
    {
        TargetHeat += delta;
    }

    protected virtual IEnumerator MoveToTargetHeat()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        float delta = 0;
        if(targetHeat > currentHeat) delta = (targetHeat - currentHeat) * heatData.HeatingSpeed; 
        else if(currentHeat > targetHeat) delta = (currentHeat - targetHeat) * heatData.CoolingSpeed;
        while(currentHeat != targetHeat)
        {
            CurrentHeat = Mathf.MoveTowards(currentHeat, targetHeat, delta); 
            if(debug) Debug.Log($"current {currentHeat} target {targetHeat} delta {delta}");
            yield return delay;
        }
    }
}