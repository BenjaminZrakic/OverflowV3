using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    [SerializeField] DamageDealer damageDealer;

    public void StartDealDamage()
    {
        damageDealer.StartDealDamage();
    }
    public void EndDealDamage()
    {
        damageDealer.EndDealDamage();
    }
}
 
