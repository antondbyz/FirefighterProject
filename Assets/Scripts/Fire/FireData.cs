using UnityEngine;

[CreateAssetMenu(menuName="New Fire Data")]
public class FireData : ScriptableObject
{
    public Vector2 MinBurningSize;
    public Vector2 MaxBurningSize;
    public float HeatToStartBurning;
}
