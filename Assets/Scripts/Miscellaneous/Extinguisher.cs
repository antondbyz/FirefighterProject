using System.Collections;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    public bool IsTurnedOn 
    {
        get => isTurnedOn;
        private set
        {
            isTurnedOn = value;
            spriteRenderer.enabled = isTurnedOn;
            if(isTurnedOn) ps.Play();
            else ps.Stop();
        }
    }

    [SerializeField] private float distance = 1;
    [SerializeField] private float efficiency = 1;
    [SerializeField] private LayerMask interactsWith = new LayerMask();
    [SerializeField] private LayerMask whatIsObstacle = new LayerMask();
    [SerializeField] private float minDistanceToObstacle = 1;

    private Transform myTransform;
    private ParticleSystem ps;
    private SpriteRenderer spriteRenderer;
    private PlayerController controller;
    private bool isTurnedOn;

    private void Awake() 
    {
        myTransform = transform;
        ps = GetComponent<ParticleSystem>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        controller = myTransform.parent.GetComponent<PlayerController>(); 
        IsTurnedOn = false;
    }

    private void OnEnable() => StartCoroutine(Extinguishing());

    private void Update() 
    {
        if(!IsTurnedOn && InputManager.ExtinguishHeld)
        {
            RaycastHit2D hit = Physics2D.Raycast(myTransform.position, myTransform.right, minDistanceToObstacle, whatIsObstacle);
            if(!controller.IsMoving && controller.IsGrounded && !hit) IsTurnedOn = true;
        }
        if(IsTurnedOn && !InputManager.ExtinguishHeld) IsTurnedOn = false;
    }

    private IEnumerator Extinguishing()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        while(true)
        {   
            yield return delay;
            if(IsTurnedOn)
            {
                RaycastHit2D hit = Physics2D.Raycast(myTransform.position, myTransform.right, distance, interactsWith);
                if(hit)
                {
                    Fire fire = hit.collider.GetComponent<Fire>();
                    if(fire != null) fire.CurrentHeat -= efficiency;
                }
            }
        }   
    }
}