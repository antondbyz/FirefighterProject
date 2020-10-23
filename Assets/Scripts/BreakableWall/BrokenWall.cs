using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private GameObject spike = null;

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Walls")) 
        {
            spike.SetActive(true);
            Destroy(gameObject);
        }   
    }
}