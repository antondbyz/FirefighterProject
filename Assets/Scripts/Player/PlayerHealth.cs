using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : Health
{
    [SerializeField] private float spikesDamage = 40;
    [SerializeField] private float spikesPushForce = 15;

    private Transform myTransform;
    private PlayerController controller;

    protected override void Awake() 
    {
        base.Awake();
        myTransform = transform;
        controller = GetComponent<PlayerController>();
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        if(other.gameObject.CompareTag("Spikes"))
        {
            CurrentHealth -= spikesDamage;
            Vector2 force = new Vector2(spikesPushForce, spikesPushForce);
            if(myTransform.position.x < other.transform.position.x) force.x = -force.x;
            controller.Push(force);
        }
    }
}
