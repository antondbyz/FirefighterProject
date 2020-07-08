using UnityEngine;

public class Wounded : MonoBehaviour
{
    public float CurrentHealth { get; set; }
    public float MaxHealth => maxHealth;

    [SerializeField] private float maxHealth = 100;
}