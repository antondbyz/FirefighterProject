using UnityEngine;

[CreateAssetMenu(menuName="My assets/New fire settings")]
public class FireSettings : ScriptableObject
{
    [Header("Particles speed")]
    public float MaxParticlesSpeed;
    public float MinParticlesSpeed;
    [Header("Particles size")]
    public float MaxParticlesSize;
    public float MinParticlesSize;
    [Header("Collider size")]
    public Vector2 MaxColliderSize;
    public Vector2 MinColliderSize;
    [Header("Collider offset")]
    public Vector2 MaxColliderOffset;
    public Vector2 MinColliderOffset;
}