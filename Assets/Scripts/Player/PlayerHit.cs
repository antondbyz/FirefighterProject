using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    [SerializeField] private float hitDistance = 1;

    private Player player;

    private void Awake() 
    {
        player = GetComponent<Player>();    
    }

    private void OnEnable() => InputManager.HitPressed += Hit;

    private void OnDisable() => InputManager.HitPressed -= Hit;

    private void Hit()
    {
        RaycastHit2D hit = player.WhatIsInFront(hitDistance);
        if(hit)
        {
            BreakableObject breakable = hit.collider.GetComponent<BreakableObject>();
            if(breakable != null) breakable.Break(player.CurrentDirection);
        }
    }
}