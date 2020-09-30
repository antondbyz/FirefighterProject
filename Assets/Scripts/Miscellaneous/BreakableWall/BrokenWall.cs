using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private GameObject spike = null;
    [SerializeField] private LayerMask whatIsGround = new LayerMask();

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(spike != null && ((whatIsGround.value & (1 << other.gameObject.layer)) == 1)) 
        {
            spike.SetActive(true);
            gameObject.SetActive(false);
        }   
    }
}