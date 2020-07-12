using UnityEngine;

[RequireComponent(typeof(Heat))]
public class FireHeatResponse : MonoBehaviour, IHeatResponse
{
    private static float minScaleValue = 0.1f;
    private static float maxScaleValue = 1f;
    private static Vector2 minScale = new Vector2(minScaleValue, minScaleValue);
    private static Vector2 maxScale = new Vector2(maxScaleValue, maxScaleValue);

    public bool IsBurning => heat.CurrentHeat > 0;

    private Heat heat;
    private HeatingZone heatingZone;
    private ParticleSystem ps;

    public void Response()
    {
        heatingZone.HeatingSpeed = heat.CurrentHeat;
        if(IsBurning)
        {
            ps.Play();
            transform.localScale = Vector2.Lerp(minScale, maxScale, heat.CurrentHeat / heat.MaxHeat);
        }
        else
        {
            ps.Stop();
            Destroy(gameObject, 2);
        }
    }

    private void Awake()
    {
        heat = GetComponent<Heat>();
        heatingZone = GetComponent<HeatingZone>();
        ps = GetComponent<ParticleSystem>();
        EnsureThatScaleIsInRange(minScaleValue, maxScaleValue);
        SetHeatAccordingToScale();
    }

    private void EnsureThatScaleIsInRange(float min, float max)
    {
        Vector2 currentScale = transform.localScale;

        if(currentScale.x < min) currentScale = new Vector2(min, currentScale.y);
        else if(currentScale.x > max) currentScale = new Vector2(max, currentScale.y);

        if(currentScale.y < min) currentScale = new Vector2(currentScale.x, min);
        else if(currentScale.y > max) currentScale = new Vector2(currentScale.x, max);

        if(currentScale.x != currentScale.y) currentScale = new Vector2(currentScale.x, currentScale.x);
        transform.localScale = currentScale;
    }

    private void SetHeatAccordingToScale()
    {
        float scaleCoefficient = (transform.localScale.x - minScaleValue) / (maxScaleValue - minScaleValue);
        heat.CurrentHeat = heat.MaxHeat * scaleCoefficient;
    }
}