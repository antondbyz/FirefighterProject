using UnityEngine;

public class ExtinguishingSubstance : MonoBehaviour
{
    public float Amount => amount;

    [Range(0, Extinguisher.MAX_SUBSTANCE_AMOUNT)] [SerializeField] private float amount = 100;
}