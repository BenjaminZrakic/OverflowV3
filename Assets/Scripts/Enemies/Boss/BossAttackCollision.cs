using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttackCollision : MonoBehaviour
{

    Boss boss;

    private void Start() {
        boss=GetComponentInParent<Boss>();
    }
    
    private void OnTriggerEnter(Collider other) {
        if(!boss.spinning){
            boss.isAttacking = true;
            //print("Attacky boi");
        }
    }

    private void OnTriggerExit(Collider other) {
        boss.isAttacking = false;
        
    }
}
