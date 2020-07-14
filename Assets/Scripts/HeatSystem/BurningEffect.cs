using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BurningEffect : MonoBehaviour 
{
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

    public void Play() => ps.Play();

    public void Stop() => ps.Stop();

    public void Scale(float coefficient)
    {
        main.gravityModifier = Mathf.Lerp(minParticlesGravity, minParticlesGravity * 2, coefficient);
        main.startSize = Mathf.Lerp(minParticlesSize, minParticlesSize * 2, coefficient);
    }
}