using UnityEngine;

public class BrokenWall : MonoBehaviour
{
    [SerializeField] private GameObject spike = null;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(spike != null && other.CompareTag("Wall")) 
        {
            spike.SetActive(true);
            gameObject.SetActive(false);
        }   
    }
}