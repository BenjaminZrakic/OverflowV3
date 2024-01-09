using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class ExplodyEnemy : Enemy
{
    public GameObject explosion;
    public GameObject explosionSpawnLocation;
 
    // Update is called once per frame
    public override void Update()
    {
        //animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
 
        if (player == null)
        {
            return;
        }
 
        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                isAttacking = true;
                timePassed = 0;
                Die();
            }
            else{
                isAttacking = false;
            }
        }

        timePassed += Time.deltaTime;
 
        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            if(!isAttacking)
                agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;

        var direction = player.transform.position - transform.position;

        // You might want to delete this line.
        // Ignore the height difference.
        direction.y = 0;

        // Make the transform look in the direction.
        transform.forward = direction;
    }
 
 
    public override void Die()
    {
        if (ragdoll != null)
            Instantiate(ragdoll, transform.position,transform.rotation);
        
        if(isWaveSpawn){
            monsterSpawner.currentMonsters.Remove(this.gameObject);
        }

        GameObject explosionObject = Instantiate(explosion, explosionSpawnLocation.transform.position, Quaternion.identity);
        Destroy(explosionObject, 3f);
        Destroy(this.gameObject);
        

    }
 

}
