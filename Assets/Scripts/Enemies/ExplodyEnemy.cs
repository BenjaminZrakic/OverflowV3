using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class ExplodyEnemy : MonoBehaviour
{
    [SerializeField] float health = 3;
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;

    public GameObject explosion;
    public GameObject explosionSpawnLocation;
 
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

    bool isAttacking = false;
 
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
        if (ragdoll != null)
            Instantiate(ragdoll, transform.position,transform.rotation);
        
        if(isWaveSpawn){
            monsterSpawner.currentMonsters.Remove(this.gameObject);
        }

        GameObject explosionObject = Instantiate(explosion, explosionSpawnLocation.transform.position, Quaternion.identity);
        Destroy(explosionObject, 3f);
        Destroy(this.gameObject);
        

    }
 
    public void TakeDamage(float damageAmount)
    {
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
        if (hitVFX != null){
            GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
            Destroy(hit, 3f);
        }
        
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
