using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private float hitDistance = 1;
    [SerializeField] private float hitForce = 300;
    [SerializeField] private LayerMask whatCanHit = new LayerMask();

    private Player player;

    private void Awake() 
    {
        player = GetComponent<Player>();    
    }

    private void Update() 
    {
        if(InputManager.HitPressed)
        {
            RaycastHit2D hit = Physics2D.Raycast(player.ColliderCenter, player.Direction, hitDistance, whatCanHit);
            if(hit)
            {
                BreakableObject breakable = hit.collider.GetComponent<BreakableObject>();
                if(breakable != null) breakable.Break(player.Direction * hitForce);
            }
        }    
    }
}