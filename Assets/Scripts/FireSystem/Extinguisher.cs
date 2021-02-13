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
    [SerializeField] private LayerMask interactsWith = new LayerMask();
    [SerializeField] private LayerMask whatIsObstacle = new LayerMask();
    [SerializeField] private float minDistanceToObstacle = 1;

    private Transform myTransform;
    private ParticleSystem ps;
    private PlayerAim aim;
    private bool isTurnedOn;
    private RaycastHit2D[] results = new RaycastHit2D[3];

    private void Awake() 
    {
        myTransform = transform;
        ps = GetComponent<ParticleSystem>();
        aim = myTransform.parent.GetComponent<PlayerAim>();
    }

    private void OnEnable() => StartCoroutine(Extinguishing());

    private void Update() 
    {
        if(aim.IsAiming)
        {
            IsTurnedOn = !Physics2D.Raycast(myTransform.position, myTransform.right, minDistanceToObstacle, whatIsObstacle);
        }
        else IsTurnedOn = false;
    }

    private IEnumerator Extinguishing()
    {
        WaitForSeconds delay = new WaitForSeconds(0.3f);
        while(true)
        {   
            yield return delay;
            if(IsTurnedOn)
            {
                Physics2D.RaycastNonAlloc(myTransform.position, myTransform.right, results, distance, interactsWith);
                for(int i = 0; i < results.Length; i++) 
                {
                    if(results[i])
                    {
                        if((whatIsObstacle.value & 1 << results[i].collider.gameObject.layer) > 0) break;
                        Fire fire = results[i].collider.GetComponent<Fire>();
                        if(fire != null) fire.CurrentHeat -= efficiency;
                    }
                }
            }
        }   
    }
}