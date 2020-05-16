using UnityEngine;

public class TransformScaler : MonoBehaviour, IScalable
{
    [SerializeField] private Vector2 minScale = new Vector2();
    [SerializeField] private Vector2 maxScale = new Vector2();
    private Transform myTransform;

    private void Awake() 
    {
        myTransform = GetComponent<Transform>();    
    }

    public void LerpScale(float coefficient)
    {
        myTransform.localScale = Vector2.Lerp(minScale, maxScale, coefficient);
    }
}