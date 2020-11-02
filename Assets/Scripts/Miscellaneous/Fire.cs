using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Fire : MonoBehaviour
{
    public const float MAX_HEAT = 100;

    public float CurrentHeat
    {
        get => currentHeat;
        set
        {
            value = Mathf.Clamp(value, 0, MAX_HEAT);
            currentHeat = value;
            main.startSize = Mathf.Lerp(minParticlesSize, maxParticlesSize, currentHeat / MAX_HEAT);
            main.startSpeed = Mathf.Lerp(minParticlesSpeed, maxParticlesSpeed, currentHeat / MAX_HEAT);
            myCollider.size = Vector2.Lerp(minColliderSize, maxColliderSize, currentHeat / MAX_HEAT);
            myCollider.offset = Vector2.Lerp(minColliderOffset, maxColliderOffset, currentHeat / MAX_HEAT);
            if(currentHeat == 0)
            {
                ps.Stop();
                Destroy(myCollider);
                Destroy(gameObject, 2);
            }
        }
    }

    [Header("Min values")]
    [SerializeField] private float minParticlesSize = 0;
    [SerializeField] private float minParticlesSpeed = 0;
    [SerializeField] private Vector2 minColliderSize = new Vector2();
    [SerializeField] private Vector2 minColliderOffset = new Vector2();

    private BoxCollider2D myCollider;
    private ParticleSystem ps;
    private MainModule main;
    private float currentHeat;

#region Max values
    private float maxParticlesSize;
    private float maxParticlesSpeed;
    private Vector2 maxColliderSize;
    private Vector2 maxColliderOffset;
#endregion

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
#region Initializing max values
        maxParticlesSize = main.startSize.constant;
        maxParticlesSpeed = main.startSpeed.constant;
        maxColliderSize = myCollider.size;
        maxColliderOffset = myCollider.offset;
#endregion
        CurrentHeat = MAX_HEAT;
    }
}