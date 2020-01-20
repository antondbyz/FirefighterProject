using UnityEngine;

[CreateAssetMenu(menuName="New Heat Data")]
public class HeatData : ScriptableObject
{
    public float HeatingSpeed => heatingSpeed;
    public float CoolingSpeed => coolingSpeed;
    [Range(0, 1)] [SerializeField] private float heatingSpeed = 0.1f, coolingSpeed = 0.1f;
}