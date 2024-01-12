using System.Collections.Generic;
using UnityEngine;
 
public class DamageDealer : MonoBehaviour
{
    bool canDealDamage;
    List<GameObject> hasDealtDamage;
 
    [SerializeField] float weaponLength;
    [SerializeField] public float weaponDamage;

    Collider attackCollider;

    public float cameraShakeIntensity = 0.5f;
    public float cameraShakeDuration = 0.2f;

    
    void Start()
    {
        canDealDamage = false;
        hasDealtDamage = new List<GameObject>();
        attackCollider = GetComponent<Collider>();
    }
 
    void Update()
    {/*
        if (canDealDamage)
        {
            RaycastHit hit;
 
            int layerMask = 1 << 7;
            if (Physics.Raycast(transform.position, -transform.up, out hit, weaponLength, layerMask))
            {
                print("Raycast hit something");
                if (hit.transform.TryGetComponent(out Enemy enemy) && !hasDealtDamage.Contains(hit.transform.gameObject))
                {
                    print("Raycast hit enemy");
                    enemy.TakeDamage(weaponDamage);
                    

                    enemy.HitVFX(hit.point);
                    hasDealtDamage.Add(hit.transform.gameObject);
                }
            }
        }*/
    }


    private void OnTriggerEnter(Collider other) {
        //print("Hit enemy");
            
        if(other.gameObject.TryGetComponent(out Enemy enemy) && !hasDealtDamage.Contains(other.gameObject)){
            enemy.TakeDamage(weaponDamage);
            hasDealtDamage.Add(other.gameObject);
            CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeDuration);
        }
        
        
    }

    public void StartDealDamage()
    {
        //Debug.Log("Player can deal damage");
        attackCollider.enabled = true;
        hasDealtDamage.Clear();
    }
    public void EndDealDamage()
    {
        //Debug.Log("Player cant deal damage");
        attackCollider.enabled = false;
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position - transform.up * weaponLength);
    }
}