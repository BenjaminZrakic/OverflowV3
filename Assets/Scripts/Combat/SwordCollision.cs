using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordCollision : MonoBehaviour
{
    [SerializeField] DamageDealer damageDealer;
    [SerializeField] private Animator anim;

    InputAction interactAction;

    public List<AttackSO> combo;
    float lastClickedTime;
    float lastComboEnd;
    int comboCounter;
    public float cooldownTime = 0.2f;
    public float spamProtection = 0.2f;
    
    private void Start() {
        interactAction = GetComponentInChildren<PlayerInput>().actions["Attack"];
        interactAction.performed += Attack;
        anim = GetComponent<Animator>();
    }

    private void Update() {
        /*if(Time.time - lastClickedTime > maxComboDelay){
            noOfClicks = 0;
        }*/
        ExitAttack();
    }

    public void StartDealDamage()
    {
        damageDealer.StartDealDamage();
    }
    public void EndDealDamage()
    {
        damageDealer.EndDealDamage();
    }

    public void Attack(InputAction.CallbackContext context)
    {   
        if (!context.performed) return;

        if(Time.time - lastComboEnd > cooldownTime && comboCounter<combo.Count){
            CancelInvoke("EndCombo");

            if(Time.time - lastClickedTime >= spamProtection){
                anim.runtimeAnimatorController = combo[comboCounter].animatorOv;
                anim.Play("Attack",0,0);
                damageDealer.weaponDamage = combo[comboCounter].damage;
                comboCounter++;
                lastClickedTime = Time.time;

                if (comboCounter >= combo.Count){
                    comboCounter = 0;
                }
            }
        }
        
    }

    void ExitAttack(){
        if(anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.9f && anim.GetCurrentAnimatorStateInfo(0).IsTag("Attack")){
            Invoke("EndCombo",1);
        }
    }

    void EndCombo(){
        comboCounter = 0;
        lastComboEnd = Time.time;
    }
}
 
