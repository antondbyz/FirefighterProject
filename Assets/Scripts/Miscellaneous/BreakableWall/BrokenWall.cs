using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private GameObject spike = null;
    [SerializeField] private LayerMask whatIsGround = new LayerMask();

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(spike != null && whatIsGround.ContainsLayer(other.gameObject.layer)) 
        {
            spike.SetActive(true);
            gameObject.SetActive(false);
        }   
    }
}