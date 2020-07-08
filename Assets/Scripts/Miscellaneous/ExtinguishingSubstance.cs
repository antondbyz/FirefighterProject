using UnityEngine;

public class ExtinguishingSubstance : MonoBehaviour
{
    [Range(0, Extinguisher.MAX_SUBSTANCE_AMOUNT)] [SerializeField] private float amount = 100;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Player player = other.GetComponent<Player>();
        if(player != null)
        {
            player.Extinguisher.CurrentSubstanceAmount += amount;    
            Destroy(gameObject);
        }
    }
}