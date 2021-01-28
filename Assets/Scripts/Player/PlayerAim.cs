using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public bool IsAiming { get; private set; }

    [SerializeField] private Transform rotateBone = null;
    [SerializeField] private GameObject extinguisherHoseDrawn = null;
    [SerializeField] private GameObject extinguisherHoseHidden = null;
    [SerializeField] private LayerMask whatIsObstacle = new LayerMask();
    [SerializeField] private float minDistanceToObstacle = 1;

    private PlayerController controller;
    private Transform myTransform;

    private void Awake() 
    {
        controller = GetComponent<PlayerController>();
        myTransform = transform; 
        StopAiming();
    }

    private void OnEnable()
    { 
        ScreenEventsHandler.PointerDown += StartAiming;
        ScreenEventsHandler.Drag += StartAiming;
        ScreenEventsHandler.Drag += UpdateRotation;
        ScreenEventsHandler.PointerUp += StopAiming;
    }

    private void OnDisable() 
    { 
        ScreenEventsHandler.PointerDown -= StartAiming;
        ScreenEventsHandler.Drag -= StartAiming;
        ScreenEventsHandler.Drag -= UpdateRotation;
        ScreenEventsHandler.PointerUp -= StopAiming;
        StopAiming();
    }

    private void StartAiming()
    {
        if(!IsAiming)
        {
            RaycastHit2D hit = Physics2D.Raycast(myTransform.position, myTransform.right, minDistanceToObstacle, whatIsObstacle);
            if(!controller.IsMoving && controller.IsGrounded && !hit)
            {
                IsAiming = true;
                extinguisherHoseDrawn.SetActive(true);
                extinguisherHoseHidden.SetActive(false);
            }
        }
    }

    private void StopAiming()
    {
        IsAiming = false;
        extinguisherHoseDrawn.SetActive(false);
        extinguisherHoseHidden.SetActive(true);
        rotateBone.localRotation = Quaternion.Euler(0, 0, 0);
    }

    private void UpdateRotation()
    {
        if(IsAiming)
        {
            Vector3 newRotation = rotateBone.localEulerAngles;
            newRotation.z += ScreenEventsHandler.DragDelta.y;
            rotateBone.localEulerAngles = newRotation;
            ClampRotation(-45, 60);  
        }
    }

    private void ClampRotation(float minRotation, float maxRotation)
    {
        Vector3 convertedRotation = rotateBone.localEulerAngles;
        if(rotateBone.localEulerAngles.z >= 180) convertedRotation.z -= 360;
        convertedRotation.z = Mathf.Clamp(convertedRotation.z, minRotation, maxRotation);
        rotateBone.localEulerAngles = convertedRotation;
    } 
}