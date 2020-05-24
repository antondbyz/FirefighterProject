using UnityEngine;

[RequireComponent(typeof(ParticleSystem), typeof(HeatingZone))]
public class Fire : HeatDependent
{
    public bool IsBurning => CurrentHeat > 0;

    private HeatingZone heatingZone;
    private ParticleSystem ps;
    private IScalable scalable;

    public override void UpdateState()
    {
        if(IsBurning)
        {
            scalable?.LerpScale(CurrentHeat / MaxHeat);
            ps.Play();
            heatingZone.StartHeatingEnteredObjects(CurrentHeat);
        }
        else
        {
            ps.Stop();
            heatingZone.StopHeatingEnteredObjects();
            Destroy(gameObject, 2);
        }
    }

    private void Awake() 
    {
        heatingZone = GetComponent<HeatingZone>();
        ps = GetComponent<ParticleSystem>();
        scalable = GetComponent<IScalable>(); 
        UpdateState();
    }
}