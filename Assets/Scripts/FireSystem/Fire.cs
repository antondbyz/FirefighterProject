using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Fire : MonoBehaviour
{
    public const float MAX_HEAT = 100;
    public static event System.Action Extinguished;

    public float CurrentHeat
    {
        get => currentHeat;
        set
        {
            value = Mathf.Clamp(value, 0, MAX_HEAT);
            if(currentHeat != value)
            {
                currentHeat = value;
                UpdateState();
                if(currentHeat == 0)
                {
                    Extinguished?.Invoke();
                    ps.Stop();
                    Destroy(myCollider);
                }
            }
        }
    }

    [Range(1, MAX_HEAT)] [SerializeField] private float currentHeat = MAX_HEAT;
    [SerializeField] private FireSettings settings = null;

    private BoxCollider2D myCollider;
    private ParticleSystem ps;
    private MainModule main;

    public void Initialize()
    {
        if(myCollider == null) myCollider = GetComponent<BoxCollider2D>();
        if(ps == null) ps = GetComponent<ParticleSystem>();
        main = ps.main;
    }

    public void UpdateState()
    {
        main.startSpeed = Mathf.Lerp(settings.MinParticlesSpeed, settings.MaxParticlesSpeed, currentHeat / MAX_HEAT);
        main.startSize = Mathf.Lerp(settings.MinParticlesSize, settings.MaxParticlesSize, currentHeat / MAX_HEAT);
        myCollider.size = Vector2.Lerp(settings.MinColliderSize, settings.MaxColliderSize, currentHeat / MAX_HEAT);
        myCollider.offset = Vector2.Lerp(settings.MinColliderOffset, settings.MaxColliderOffset, currentHeat / MAX_HEAT);
    }

    private void Awake() => Initialize();
}