using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{

    public bool phaseTwo = false;
    public bool spinning = false;
    public Animator attackAnimator;

    public float rotationSpeed = 10f;

    public int attackCounter = 0;

    public bool inAttackAnimation = false;

    public Vector3 spawnPoint;

    public override void Start(){
        base.Start();
        spawnPoint = transform.position;

    }

    public override void Update() {
        //print(isAttacking);
        if (player == null)
        {
            return;
        }
        
        if(attackCounter>=5){
            attackCounter=0;
            StartSpinning();
        }

        if (timePassed >= attackCD)
        {
            if (isAttacking){
                animator.SetTrigger("attack");
                timePassed = 0;
            }
            else{
                animator.ResetTrigger("attack");
            }
            
        }

        /*
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
        }*/
        

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

        Quaternion rot = Quaternion.LookRotation(direction);

        if(!isAttacking && !inAttackAnimation)
            //transform.forward = direction;
        
            
            transform.rotation = Quaternion.Lerp(transform.rotation, rot, rotationSpeed * Time.deltaTime);
    }

    public void StartAttack(){
        inAttackAnimation = true;
    }

    public void EndAttack(){
        attackCounter++;
        inAttackAnimation = false;
    }

    public void StartSpinning(){
        attackAnimator.SetTrigger("spinning");
        spinning = true;
    }


    
    
    private void OnTriggerEnter(Collider other) {
        if(!spinning){
            isAttacking = true;
            //print("Attacky boi");
        }
    }

    private void OnTriggerExit(Collider other) {
        isAttacking = false;
        
    }
}
