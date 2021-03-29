using UnityEngine;

public class Checkpoint : MonoBehaviour 
{
    public bool IsActive
    {
        get => isActive;
        set
        {
            if(!isActive && value) activateSound.Play(); 
            isActive = value;
            exitSign.color = isActive ? activeColor : notActiveColor;
        }
    }

    [SerializeField] private SpriteRenderer exitSign = null;
    [SerializeField] private Color activeColor = Color.white;
    [SerializeField] private Color notActiveColor = Color.white;
    [SerializeField] private Sound activateSound = null;

    private bool isActive;

    private void OnTriggerEnter2D(Collider2D other) 
    {
        Player player = other.GetComponent<Player>();    
        player.CurrentCheckpoint = this;
    }
}