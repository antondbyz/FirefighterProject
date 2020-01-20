using UnityEngine;

public class FireExtinguisher : MonoBehaviour
{
    public bool IsExtinguishing { get; private set; } = false;
    public bool FlipX
    {
        get { return flipX; }
        set
        {
            flipX = value;

            float angle;
            if(flipX) angle = Vector2.Angle(Vector2.up, transform.up);
            else angle = -Vector2.Angle(Vector2.up, transform.up);

            transform.localRotation = Quaternion.Euler(transform.localRotation.x, transform.localRotation.y, angle);
            transform.localPosition = new Vector3(-transform.localPosition.x, transform.localPosition.y, transform.localPosition.z);
        }
    }

    [SerializeField] [Range(0, 180)] private float maxRotateAngle = 120, minRotateAngle = 40;

    private ExtinguishingSubstance substance;
    private bool flipX;

    private void Awake()
    {
        substance = transform.GetChild(0).GetComponent<ExtinguishingSubstance>();
        GameManager.OnPaused.AddListener(TurnOff);
        SwipeHandler.OnSwipe.AddListener(Rotate);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) TurnOn();
        else if(Input.GetKeyUp(KeyCode.E)) TurnOff();
    }

    private void Rotate()
    {
        if(IsExtinguishing)
        {
            if(flipX)
            {
                float target = SwipeHandler.Delta.y > 0 ? minRotateAngle : maxRotateAngle;
                float angle = Mathf.MoveTowards(transform.rotation.eulerAngles.z, target, Mathf.Abs(SwipeHandler.Delta.y));
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
            else
            {
                float target = SwipeHandler.Delta.y > 0 ? minRotateAngle : maxRotateAngle;
                float angle = Mathf.MoveTowards(transform.rotation.eulerAngles.z, 360 - target, Mathf.Abs(SwipeHandler.Delta.y));
                transform.rotation = Quaternion.Euler(0, 0, angle);
            }
        }
    }

    public void TurnOn()
    {
        IsExtinguishing = true;
        substance.StartEmit();
    }

    public void TurnOff()
    {
        IsExtinguishing = false;
        substance.StopEmit();
    }
}