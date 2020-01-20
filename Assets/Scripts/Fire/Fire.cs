using UnityEngine;

public class Fire : MonoBehaviour
{
    private enum Action { DESTROY, IGNORE }
    public bool IsBurning => burnable.CurrentHeat >= data.HeatToStartBurning;
    public Burnable Burnable => burnable;
    [SerializeField] private Burnable burnable = null;
    [SerializeField] private FireData data = null;
    [SerializeField] private Action onStopBurning = Action.IGNORE;
    private ParticleSystem ps;
    private new Collider2D collider;

    private void Awake() 
    {
        ps = GetComponent<ParticleSystem>();    
        collider = GetComponent<Collider2D>();
        burnable.OnHeatChanged.AddListener(UpdateBurning);
    }

    public void UpdateBurning()
    {
        if(IsBurning)
        {
            if(!ps.isPlaying) ps.Play();
            float t = (burnable.CurrentHeat - data.HeatToStartBurning) / 100;
            transform.localScale = Vector2.Lerp(data.MinBurningSize, data.MaxBurningSize, t);
        }
        else 
        {
            if(ps.isPlaying) ps.Stop();
            if(burnable.TargetHeat < data.HeatToStartBurning)
                if(onStopBurning == Action.DESTROY) Destroy(2);
        }
    }

    private void Destroy(float delay)
    {
        burnable.OnHeatChanged.RemoveListener(UpdateBurning);
        Destroy(gameObject, delay);
    }
}