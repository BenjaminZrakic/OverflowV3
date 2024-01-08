using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : Interactable
{
    public GameObject spawnLocation; 
    public LevelManager levelManager;
    public GameObject player;

    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();
    }

    // Update is called once per frame
    protected override void Interaction()
    {
        base.Interaction();
        print("Interacted with totem");
        var healthSystem = player.GetComponent<HealthSystem>();
        healthSystem.healingCharges = healthSystem.maxHealingCharges;
        healthSystem.UpdateHealingChargesLabel();
        var healthSystem2 = player.GetComponent<HealthSystemForDummies>();
        healthSystem2.AddToCurrentHealth(healthSystem2.MaximumHealth);

        levelManager.spawnLocation = spawnLocation.transform.position;
    }
}

