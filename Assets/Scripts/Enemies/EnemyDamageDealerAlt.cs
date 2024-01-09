using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyDamageDealerAlt : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;
 
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;
    
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
    }


    private void OnTriggerEnter(Collider other) {
        if (!hasDealtDamage)
        {
            if (other.TryGetComponent(out HealthSystem health))
                {
                    print("Hitting player");
                    health.TakeDamage(weaponDamage);
                    health.HitVFX(other.gameObject.transform.position);
                    hasDealtDamage = true;
                }
        }
    }

    private void OnTriggerExit(Collider other) {
        hasDealtDamage = false;
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}