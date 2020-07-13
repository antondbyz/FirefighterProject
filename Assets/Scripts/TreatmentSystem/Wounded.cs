using UnityEngine;

public class Wounded : MonoBehaviour
{
    public event System.Action Recovered;
    public float TimeToRecover => timeToRecover;

    [SerializeField] private float timeToRecover = 10;

    private Animator animator;
    private Collider2D myCollider;

    public void Recover()
    {
        Recovered?.Invoke();
        animator.SetBool("Idle", true);
        Destroy(myCollider);
        Destroy(gameObject, 5);
    }

    private void Awake() 
    {
        animator = GetComponent<Animator>();
        myCollider = GetComponent<Collider2D>();
    }
}