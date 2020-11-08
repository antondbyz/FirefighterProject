using System.Collections;
using UnityEngine;

public class BrokenObject : MonoBehaviour 
{
    [SerializeField] private float lifetime = 1;

    private void Awake() 
    {
        StartCoroutine(DestroySelf());
    }    

    private IEnumerator DestroySelf()
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
}