using System.Collections;
using UnityEngine;

public class SwitchableParticleSystem : MonoBehaviour
{
    [SerializeField] private float playDuration = 1;
    [SerializeField] private float stopDuration = 1;

    private ParticleSystem ps;

    private IEnumerator Start() 
    {
        ps = GetComponent<ParticleSystem>();
        WaitForSeconds playDelay = new WaitForSeconds(stopDuration);
        WaitForSeconds stopDelay = new WaitForSeconds(playDuration);
        while(true)
        {
            ps.Play();
            yield return stopDelay;
            ps.Stop();
            yield return playDelay;    
        }
    }
}
