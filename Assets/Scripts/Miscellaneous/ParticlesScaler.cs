using UnityEngine;
using static UnityEngine.ParticleSystem;

public class ParticlesScaler : MonoBehaviour
{
    private MainModule main;
    private float minParticlesGravity;
    private float minParticlesSize;

    private void Awake() 
    {
        main = GetComponent<ParticleSystem>().main;
        minParticlesGravity = main.gravityModifier.constant;
        minParticlesSize = main.startSize.constant;    
    }

    public void Scale(float coefficient)
    {
        main.gravityModifier = Mathf.Lerp(minParticlesGravity, minParticlesGravity * 2, coefficient);
        main.startSize = Mathf.Lerp(minParticlesSize, minParticlesSize * 2, coefficient);
    }
}