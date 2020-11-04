using System.Collections;
using UnityEngine;

public class Extinguisher : MonoBehaviour
{
    public bool IsTurnedOn 
    {
        get => isTurnedOn;
        private set
        {
            if(isTurnedOn != value)
            {
                isTurnedOn = value;
                if(isTurnedOn) ps.Play();
                else ps.Stop();
            }
        }
    }

    [SerializeField] private float distance = 1;
    [SerializeField] private float efficiency = 1;

    private Transform myTransform;
    private ParticleSystem ps;
    private PlayerAim aim;
    private bool isTurnedOn;

    private void Awake() 
    {
        myTransform = transform;
        ps = GetComponent<ParticleSystem>();
        aim = transform.parent.GetComponent<PlayerAim>();
    }

    private void OnEnable() => StartCoroutine(Extinguishing());

    private void Update() 
    {
        IsTurnedOn = InputManager.ExtinguishHeld && aim.IsAiming;  
    }

    private IEnumerator Extinguishing()
    {
        WaitForSeconds delay = new WaitForSeconds(0.5f);
        while(true)
        {   
            yield return delay;
            if(IsTurnedOn)
            {
                RaycastHit2D hit = Physics2D.Raycast(myTransform.position, myTransform.right, distance);
                if(hit) 
                {
                    Fire fire = hit.collider.GetComponent<Fire>();
                    if(fire != null) fire.CurrentHeat -= efficiency;
                }
            }
        }   
    }
}