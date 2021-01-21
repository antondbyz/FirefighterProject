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
                BreakableObstacle obstacle = hit.collider.GetComponent<BreakableObstacle>();
                if(obstacle != null) obstacle.Break(player.Direction * hitForce);
            }
        }    
    }
}