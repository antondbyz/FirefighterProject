using UnityEngine;

public class Player : MonoBehaviour
{
    public Damageable Damageable { get; private set; }

    [SerializeField] [Range(0, 10)] private float movementSpeed = 1;
    [SerializeField] private FireExtinguisher fireExtinguisher = null;

    private UIManager uiManager;
    private Rigidbody2D rigid;
    private SpriteRenderer rend;
    private Vector2 movement;

    private void Awake()
    {
        Damageable = GetComponent<Damageable>();
        uiManager = GameObject.FindWithTag("GameController").GetComponent<UIManager>();
        rigid = GetComponent<Rigidbody2D>();
        rend = GetComponent<SpriteRenderer>();
    }   

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.E)) fireExtinguisher.TurnOn();
        else if(Input.GetKeyUp(KeyCode.E)) fireExtinguisher.TurnOff();
    }

    public void MoveRight() 
    {
        if(!fireExtinguisher.IsExtinguishing) // Player can't move while ExtinguishingButton holding
        {
            movement.x = movementSpeed;
            movement.y = rigid.velocity.y;
            rigid.velocity = movement;
            FlipX(false);
        }
    }

    public void MoveLeft()
    {
        if(!fireExtinguisher.IsExtinguishing) // Player can't move while ExtinguishingButton holding
        {
            movement.x = -movementSpeed;
            movement.y = rigid.velocity.y;
            rigid.velocity = movement;
            FlipX(true);
        }
    }

    public void StopX()
    {
        movement.x = 0;
        movement.y = rigid.velocity.y;
        rigid.velocity = movement;
    }

    public void FlipX(bool flipX)
    {
        if(rend.flipX != flipX) rend.flipX = flipX;
        if(fireExtinguisher.FlipX != flipX) fireExtinguisher.FlipX = flipX;
    }
}