using System.Collections;
using UnityEngine;

public class BreakableWall : MonoBehaviour
{
    [SerializeField] private GameObject brokenWall = null;
    [SerializeField] private ParticleSystem wallBreakingEffect = null;
    [SerializeField] private float delay = 1;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        if(other.CompareTag("PlayerCharacter")) 
        {
            StartCoroutine(Break());
        }
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("PlayerCharacter")) 
        {
            StartCoroutine(Break());
        }
    }

    private IEnumerator Break()
    {
        wallBreakingEffect.Play();
        yield return new WaitForSeconds(delay);
        wallBreakingEffect.Stop();
        brokenWall.SetActive(true);
        Destroy(gameObject);
    }
}