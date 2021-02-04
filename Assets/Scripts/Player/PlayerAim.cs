using UnityEngine;

public class PlayerAim : MonoBehaviour
{
    public bool IsAiming { get; private set; }

    [SerializeField] private Transform rotateBone = null;
    [SerializeField] private GameObject extinguisherHoseDrawn = null;
    [SerializeField] private GameObject extinguisherHoseHidden = null;
    [SerializeField] private float rotationSpeedMultiplier = 1;
    [SerializeField] private float maxRotationAngle = 60;
    [SerializeField] private float minRotationAngle = -45;

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
            if(!controller.IsMoving && controller.IsGrounded)
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
            if(rotateBone.localEulerAngles.z >= 180) newRotation.z -= 360;
            newRotation.z += ScreenEventsHandler.DragDelta.y * rotationSpeedMultiplier;
            newRotation.z = Mathf.Clamp(newRotation.z, minRotationAngle, maxRotationAngle);
            rotateBone.localEulerAngles = newRotation;
        }
    }
}