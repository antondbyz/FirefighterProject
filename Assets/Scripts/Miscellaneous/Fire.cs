using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Fire : MonoBehaviour
{
    [System.Serializable]
    private struct Constraint
    {
        public float Min;
        public float Max;

        public Constraint(float min, float max)
        {
            Min = min;
            Max = max;
        }
    }

    public const float MAX_HEAT = 100;

    public float CurrentHeat
    {
        get => currentHeat;
        set
        {
            value = Mathf.Clamp(value, 0, MAX_HEAT);
            currentHeat = value;
            main.startSize = Mathf.Lerp(particlesSize.Min, particlesSize.Max, currentHeat / MAX_HEAT);
            main.startSpeed = Mathf.Lerp(particlesSpeed.Min, particlesSpeed.Max, currentHeat / MAX_HEAT);
            float newColliderSizeY = Mathf.Lerp(colliderSizeY.Min, colliderSizeY.Max, currentHeat / MAX_HEAT);
            float newColliderOffsetY = Mathf.Lerp(colliderOffsetY.Min, colliderOffsetY.Max, currentHeat / MAX_HEAT);
            myCollider.size = new Vector2(myCollider.size.x, newColliderSizeY);
            myCollider.offset = new Vector2(myCollider.offset.x, newColliderOffsetY);
            if(currentHeat == 0)
            {
                ps.Stop();
                Destroy(myCollider);
                Destroy(gameObject, 2);
            }
        }
    }

    [SerializeField] private Constraint particlesSize = new Constraint();
    [SerializeField] private Constraint particlesSpeed = new Constraint();
    [SerializeField] private Constraint colliderSizeY = new Constraint();
    [SerializeField] private Constraint colliderOffsetY = new Constraint();

    private BoxCollider2D myCollider;
    private ParticleSystem ps;
    private MainModule main;
    private float currentHeat;

    private void Awake()
    {
        myCollider = GetComponent<BoxCollider2D>();
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
        CurrentHeat = MAX_HEAT;
    }
}