using UnityEngine;

public class ExtinguishingSubstance : MonoBehaviour
{
    [SerializeField] private float amount = 100;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        Player player = other.gameObject.GetComponent<Player>();
        if(player != null)
        {
            player.Extinguisher.CurrentSubstanceAmount += amount;    
            Destroy(gameObject);
        }
    }
}