using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
 
public class Enemy : MonoBehaviour
{
    [SerializeField] protected GameObject hitVFX;
    [SerializeField] protected GameObject ragdoll;
 
    [Header("Combat")]
    [SerializeField] protected float attackCD = 3f;
    [SerializeField] protected float attackRange = 1f;
    [SerializeField] protected float aggroRange = 4f;
 
    protected GameObject player;
    protected NavMeshAgent agent;
    protected Animator animator;
    protected HealthSystemForDummies healthSystem;
    protected float timePassed;
    protected float newDestinationCD = 0.5f;

    [HideInInspector]
    public bool isWaveSpawn = false;
    public MonsterSpawner monsterSpawner;

    public bool isAttacking = false;

    protected Vector3 direction;
 
    public virtual void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        healthSystem = GetComponent<HealthSystemForDummies>();
    }
 
    // Update is called once per frame
    public virtual void Update()
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
                animator.SetTrigger("attack");
                timePassed = 0;
                
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

        direction = player.transform.position - transform.position;

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
 
    public virtual void Die()
    {
        if (ragdoll != null)
            Instantiate(ragdoll, transform.position,transform.rotation);
        
        if(isWaveSpawn){
            monsterSpawner.currentMonsters.Remove(this.gameObject);
        }

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

}
