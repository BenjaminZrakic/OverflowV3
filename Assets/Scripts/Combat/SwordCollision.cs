using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SwordCollision : MonoBehaviour
{
    [SerializeField] DamageDealer damageDealer;
    [SerializeField] private Animator playerAnimator;

    InputAction interactAction;

    private void Start() {
        interactAction = GetComponentInChildren<PlayerInput>().actions["Attack"];
        interactAction.performed += Attack;
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

        if (!playerAnimator.GetBool("isAttacking")) playerAnimator.SetTrigger("AttackPerformed");


        Debug.Log("Attack");
    }
}
 
