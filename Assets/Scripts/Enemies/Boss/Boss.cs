using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public bool phaseTwo = false;
    public bool spinning = false;
    public Animator attackAnimator;

    public float rotationSpeed = 10f;


    public override void Start(){
        base.Start();

    }

    public override void Update() {
        print(isAttacking);
        if (player == null)
        {
            return;
        }

        if(!spinning){
            if (timePassed >= attackCD)
            {
                if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
                {
                    isAttacking = true;
                    animator.SetTrigger("attack");
                    print("Attacky boi");
                    timePassed = 0;
                    
                }
                else{
                    isAttacking = false;
                }
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

        direction = player.transform.position - transform.position;

        // You might want to delete this line.
        // Ignore the height difference.
        direction.y = 0;

        if(!isAttacking)
            transform.forward = direction;
    }

    public void StartSpinning(){
        attackAnimator.SetTrigger("spinning");
        spinning = true;
    }

    public void StartPhaseTwo(){
        if (healthSystem.CurrentHealthPercentage <= 50 && !phaseTwo){
            animator.SetTrigger("phaseTwo");
        }
    }
}
