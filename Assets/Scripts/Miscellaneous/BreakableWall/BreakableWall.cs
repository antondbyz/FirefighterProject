using System.Collections;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private GameObject destroyedWall = null;
    [SerializeField] private ParticleSystem wallDestroyingEffect = null;
    [SerializeField] private float delay = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("PlayerCharacter")) 
        {
            StartCoroutine(Destroy());
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("PlayerCharacter")) 
        {
            StartCoroutine(Destroy());
        }
    }

    private IEnumerator Destroy()
    {
        wallDestroyingEffect.Play();
        yield return new WaitForSeconds(delay);
        wallDestroyingEffect.Stop();
        destroyedWall.SetActive(true);
        Destroy(gameObject);
    }
}