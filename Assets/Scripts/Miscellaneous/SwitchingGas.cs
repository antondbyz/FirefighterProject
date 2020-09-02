using System.Collections;
using UnityEngine;

public class SwitchingGas : MonoBehaviour 
{
    private Collider2D coll;
    private ParticleSystem ps;

    private void Awake() 
    {
        coll = GetComponent<Collider2D>();
        ps = GetComponent<ParticleSystem>();
        StartCoroutine(Switching());
    }

    private IEnumerator Switching()
    {
        WaitForSeconds delay = new WaitForSeconds(3);
        while(true)
        {
            ps.Play();
            coll.enabled = true;
            yield return delay;
            ps.Stop();
            coll.enabled = false;
            yield return delay;
        }
    }    
}