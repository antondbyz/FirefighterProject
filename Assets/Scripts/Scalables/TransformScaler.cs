using UnityEngine;

public class TransformScaler : MonoBehaviour, IScalable
{
    [SerializeField] private Vector2 minScale = new Vector2();
    [SerializeField] private Vector2 maxScale = new Vector2();

    public void LerpScale(float coefficient)
    {
        transform.localScale = Vector2.Lerp(minScale, maxScale, coefficient);
    }
}