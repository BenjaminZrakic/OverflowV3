using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class RangedEnemy : Enemy
{


    public GameObject projectile;
    public GameObject projectileShot;

    public float shotForce = 10;


 
    public void FireProjectile(){
        GameObject projectileShotObject = Instantiate(projectileShot,projectile.gameObject.transform.position,Quaternion.identity);
        projectileShotObject.transform.forward = direction;
        projectileShotObject.GetComponent<Rigidbody>().AddForce(direction.normalized * shotForce, ForceMode.Impulse);
    }

}
