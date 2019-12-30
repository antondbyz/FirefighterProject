using UnityEngine;

[CreateAssetMenu(menuName="Fire/New Fire")]
public class FireSettings : ScriptableObject
{
    [System.Serializable]
    public struct SizeConstraint
    {
        public float MaxSize, MinSize;
    }

    [System.Serializable]
    public struct DamageConstraint
    {
        public float MaxDamage, MinDamage;
    }

    [System.Serializable]
    public struct SparksConstraints
    {
        [Header("Up force constraint")]
        public float MaxYForce;
        public float MinYForce; 
        [Header("Side force constraint")]
        public float MaxXForce;
        public float MinXForce;
        [Header("Frequency constraint")]
        [Range(0, 5)] public float MaxFrequency;
        [Range(0, 5)] public float MinFrequency;
    }
    
    public SizeConstraint Size;
    public DamageConstraint Damage;
    public SparksConstraints Sparks;
}