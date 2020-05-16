using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticlesScaler : MonoBehaviour, IScalable
{
    [SerializeField] private float scaleMultiplier = 2;

    private ParticleSystem ps;
    private MainModule main;
    private float minParticlesGravity;
    private float minParticlesSize;

    private void Awake() 
    {
        ps = GetComponent<ParticleSystem>();
        main = ps.main;
        minParticlesGravity = main.gravityModifier.constant;
        minParticlesSize = main.startSize.constant;    
    }

    public void LerpScale(float coefficient)
    {
        main.gravityModifier = Mathf.Lerp(minParticlesGravity, minParticlesGravity * scaleMultiplier, coefficient);
        main.startSize = Mathf.Lerp(minParticlesSize, minParticlesSize * scaleMultiplier, coefficient);
    }
}