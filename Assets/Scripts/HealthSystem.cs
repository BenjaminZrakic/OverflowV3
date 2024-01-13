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

    public int maxHealingCharges = 3;
    public int healingCharges = 3;

    public float cameraShakeIntensity = 0.5f;
    public float cameraShakeDuration = 0.2f;

    public TMP_Text healingChargesLabel;

    public GameObject playerVisual;
    public LevelManager levelManager;
    
    void Start()
    {
        animator = GetComponent<Animator>();
        healthSystem = GetComponent<HealthSystemForDummies>();
        levelManager = GameObject.Find("LevelManager").GetComponent<LevelManager>();

        healingAmount = ((int)healthSystem.MaximumHealth)/4;
        healAction = GetComponent<PlayerInput>().actions["Heal"];
        healAction.performed += Heal;
        healingChargesLabel.text = healingCharges.ToString();
    }



 
    public void TakeDamage(float damageAmount)
    {
        CameraShake.Instance.ShakeCamera(cameraShakeIntensity, cameraShakeDuration);
        healthSystem.AddToCurrentHealth(-damageAmount);

    }

    public void Heal(InputAction.CallbackContext obj){
        if( healingCharges > 0 && healthSystem.CurrentHealth != healthSystem.MaximumHealth){
            print("Healing player");
            healthSystem.AddToCurrentHealth(healingAmount);
            healingCharges--;
            UpdateHealingChargesLabel();
        }
        else{
            print("No healing charges left");
        }
        
    }

    public void UpdateHealingChargesLabel(){
        healingChargesLabel.text = healingCharges.ToString();
    }
 
    public void Die(bool isAlive)
    {   
        
        if(!isAlive){
            if(levelManager.inBossFight){
                levelManager.ResetBoss();
            }
        
            gameObject.transform.position = levelManager.spawnLocation;
            healthSystem.AddToCurrentHealth(healthSystem.MaximumHealth);
        }
        
       
        //Instantiate(ragdoll, transform.position, transform.rotation);
        //Destroy(this.gameObject);
    }
    public void HitVFX(Vector3 hitPosition)
    {
        GameObject hit = Instantiate(hitVFX, hitPosition, Quaternion.identity);
        Destroy(hit, 3f);
 
    }
}
 