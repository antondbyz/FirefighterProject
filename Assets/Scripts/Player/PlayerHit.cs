using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private float hitDistance = 1;
    [SerializeField] private float hitForce = 300;
    [SerializeField] private LayerMask whatCanHit = new LayerMask();

    private Transform myTransform;
    private Collider2D myCollider;

    private void Awake() 
    {
        myTransform = transform;  
        myCollider = GetComponent<Collider2D>();  
    }

    private void Update() 
    {
        if(InputManager.HitPressed)
        {
            RaycastHit2D hit = Physics2D.Raycast(myCollider.bounds.center, myTransform.right, hitDistance, whatCanHit);
            if(hit)
            {
                BreakableObstacle obstacle = hit.collider.GetComponent<BreakableObstacle>();
                if(obstacle != null) obstacle.Break(myTransform.right * hitForce);
            }
        }    
    }
}