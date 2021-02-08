using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ElectricStrike : MonoBehaviour 
{
    private MainModule main;
    private VelocityOverLifetimeModule velocity;

    private void Awake() 
    {
        ParticleSystem ps = GetComponent<ParticleSystem>();
        main = ps.main;
        velocity = ps.velocityOverLifetime;
    }

    private void Update() 
    {
        if((Time.time % main.duration) <= 0.01f)
            velocity.speedModifier = 1;
    }

    private void OnParticleCollision(GameObject other) 
    {
        velocity.speedModifier = 0;
    }    
}