using System.Collections;
using UnityEngine;

public class PlayAnimationWithDelay : MonoBehaviour
{
    [SerializeField] private float delay = 1;

    private Animation myAnimation;

    private void Awake() 
    {
        myAnimation = GetComponent<Animation>();
        StartCoroutine(PlayWithDelay());
    }   

    private IEnumerator PlayWithDelay()
    {
        yield return new WaitForSeconds(delay);
        myAnimation.Play();
    }
}