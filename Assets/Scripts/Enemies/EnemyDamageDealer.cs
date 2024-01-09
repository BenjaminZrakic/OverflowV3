using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class EnemyDamageDealer : MonoBehaviour
{
    bool canDealDamage;
    bool hasDealtDamage;
 
    [SerializeField] float weaponLength;
    [SerializeField] float weaponDamage;

    Collider attackCollider;
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = false;
        attackCollider = GetComponent<Collider>();
    }
 
    // Update is called once per frame
    void Update()
    {
        if (canDealDamage && !hasDealtDamage)
        {
            
            /*
            RaycastHit hit;
 
            int layerMask = 1 << 3;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                
                if (hit.transform.TryGetComponent(out HealthSystem health))
                {
                    print("Hitting player");
                    health.TakeDamage(weaponDamage);
                    health.HitVFX(hit.point);
                    hasDealtDamage = true;
                }
            }*/

        }
    }

    private void OnTriggerEnter(Collider other) {
        if (!hasDealtDamage)
        {
            if (other.TryGetComponent(out HealthSystem health))
                {
                    //print("Hitting player");
                    health.TakeDamage(weaponDamage);
                    health.HitVFX(other.gameObject.transform.position);
                    hasDealtDamage = true;
                }
        }
    }

    public void StartDealDamage()
    {
        //Debug.Log("Enemy dealing damage");
        attackCollider.enabled = true;
        hasDealtDamage = false;
    }
    public void EndDealDamage()
    {
        attackCollider.enabled = false;
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}