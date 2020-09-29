using UnityEngine;

[RequireComponent(typeof(HingeJoint2D))]
public class SwingingWire : MonoBehaviour
{
    [SerializeField] private float speed = 100;
    [SerializeField] private float maxRotationAngle = 60;
    [SerializeField] private float hardness = 1;

    private Rigidbody2D rb;
    private HingeJoint2D hinge;
    private JointMotor2D newMotor = new JointMotor2D();
    private int dir = 1;

    private void Awake() 
    {
        rb = GetComponent<Rigidbody2D>();
        hinge = GetComponent<HingeJoint2D>();
        newMotor.maxMotorTorque = 10000;
    }

    private void Update() 
    {
        float rotation = rb.rotation;
        if(rotation >= 180) rotation -= 360;
        if(rotation <= -maxRotationAngle) dir = -1;
        else if(rotation >= maxRotationAngle) dir = 1;
        newMotor.motorSpeed = (maxRotationAngle - Mathf.Abs(rotation) + hardness) / maxRotationAngle * speed * dir;
        hinge.motor = newMotor;
    }
}
