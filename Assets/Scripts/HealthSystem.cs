using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
 
public class HealthSystem : MonoBehaviour
{
    [SerializeField] GameObject hitVFX;
    [SerializeField] GameObject ragdoll;

    Animator animator;
 
    HealthSystemForDummies healthSystem;
    InputAction healAction;
    int healingAmount;
    public int healingCharges = 3;

    public TMP_Text healingChargesLabel;

    public GameObject playerVisual;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystemForDummies>();
        healingAmount = ((int)healthSystem.MaximumHealth)/4;
        healAction = GetComponent<PlayerInput>().actions["Heal"];
        healAction.performed += Heal;
        healingChargesLabel.text = healingCharges.ToString();
    }



 
    public void TakeDamage(float damageAmount)
    {
        /*health -= damageAmount;
        Debug.Log("Player took damage, health: "+health);
        //animator.SetTrigger("damage");
        CameraShake.Instance.ShakeCamera(2f, 0.2f);
 
        if (health <= 0)
        {
            Die();
        }*/
        healthSystem.AddToCurrentHealth(-damageAmount);

    }

    public void Heal(InputAction.CallbackContext obj){
        if( healingCharges > 0 && healthSystem.CurrentHealth != healthSystem.MaximumHealth){
            print("Healing player");
            healthSystem.AddToCurrentHealth(healingAmount);
            healingCharges--;
            healingChargesLabel.text = healingCharges.ToString();
        }
        else{
            print("No healing charges left");
        }
        
    }
 
    public void Die()
    {
        //Instantiate(ragdoll, transform.position, transform.rotation);
        //Destroy(this.gameObject);
    }
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
 
    }
}
 