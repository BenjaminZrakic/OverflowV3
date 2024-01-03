using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class Enemy : MonoBehaviour
{
    [SerializeField] float health = 3;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;
 
    [Header("Combat")]
    [SerializeField] float attackCD = 3f;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float aggroRange = 4f;
 
    GameObject player;
    NavMeshAgent agent;
    Animator animator;
    HealthSystemForDummies healthSystem;
    float timePassed;
    float newDestinationCD = 0.5f;

    [HideInInspector]
    public bool isWaveSpawn = false;
    public MonsterSpawner monsterSpawner;
 
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = GetComponent<HealthSystemForDummies>();
    }
 
    // Update is called once per frame
    void Update()
    {
        animator.SetFloat("speed", agent.velocity.magnitude / agent.speed);
 
        if (player == null)
        {
            return;
        }
 
        if (timePassed >= attackCD)
        {
            if (Vector3.Distance(player.transform.position, transform.position) <= attackRange)
            {
                animator.SetTrigger("attack");
                timePassed = 0;
            }
        }
        timePassed += Time.deltaTime;
 
        if (newDestinationCD <= 0 && Vector3.Distance(player.transform.position, transform.position) <= aggroRange)
        {
            newDestinationCD = 0.5f;
            agent.SetDestination(player.transform.position);
        }
        newDestinationCD -= Time.deltaTime;
        transform.LookAt(player.transform);
    }
 
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            print(true);
            player = collision.gameObject;
        }
    }
 
    public void Die()
    {
        Instantiate(ragdoll, transform.position,transform.rotation);
        
        if(isWaveSpawn){
            monsterSpawner.currentMonsters.Remove(this.gameObject);
        }

        Destroy(this.gameObject);
        

    }
 
    public void TakeDamage(float damageAmount)
    {
        /*
        health -= damageAmount;
        animator.SetTrigger("damage");
        CameraShake.Instance.ShakeCamera(2f, 0.2f);
 
        if (health <= 0)
        {
            Die();
        }*/
        
        // Damage enemy for damageAmount
        healthSystem.AddToCurrentHealth(-damageAmount);
    }
    public void StartDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().StartDealDamage();
    }
    public void EndDealDamage()
    {
        GetComponentInChildren<EnemyDamageDealer>().EndDealDamage();
    }
 
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
    }
 
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, aggroRange);
    }

    public void OnIsAliveChanged(bool value)
    {
        if (value == false){
            Die();
        }
        
    }

}
