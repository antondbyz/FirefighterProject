using UnityEngine;

[CreateAssetMenu(menuName="Temperature/New Temperature Data")]
public class TemperatureData : ScriptableObject
{
    [SerializeField] private float maxNormalTemperature = 50f;
}
