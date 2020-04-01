using UnityEngine;
using static UnityEngine.ParticleSystem;

public class BurningEffect : MonoBehaviour
{
    private HeatController heatController;
    private ParticleSystem ps;
    private MainModule particlesMain;
    private float minParticlesGravity, maxParticlesGravity;
    private float minParticlesSize, maxParticlesSize;

    private void Awake()
    {
        heatController = transform.parent.GetComponent<HeatController>();
        ps = GetComponent<ParticleSystem>();
        particlesMain = ps.main;
        minParticlesGravity = particlesMain.gravityModifier.constant;
        minParticlesSize = particlesMain.startSize.constant;
        maxParticlesGravity = minParticlesGravity * 2;
        maxParticlesSize = minParticlesSize * 2;
        heatController.OnHeatChanged += UpdateBurning;
    }

    private void UpdateBurning()
    {
        float t = (heatController.CurrentHeat - HeatController.HEAT_FOR_BURNING) / (Heat.MAX_HEAT - HeatController.HEAT_FOR_BURNING);
        if(t > 0)
        {
            if(!ps.isPlaying) ps.Play();
            particlesMain.gravityModifier = Mathf.Lerp(minParticlesGravity, maxParticlesGravity, t);
            particlesMain.startSize = Mathf.Lerp(minParticlesSize, maxParticlesSize, t);
        } 
        else if(ps.isPlaying) ps.Stop();
    }
}