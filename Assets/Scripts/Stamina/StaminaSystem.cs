using UnityEngine;
using System.Collections;

public class StaminaSystem : MonoBehaviour
{
    public float CurrentStamina = 100;
    public float MaximumStamina = 100;

    public bool HasAnimationWhenStaminaChanges = true;
    public float AnimationDuration = 0.1f;

    public Coroutine recharge;
    public float ChargeRate = 33;
    public float ChargeCooldown = 1f;

    public float CurrentStaminaPercentage
    {
        get
        {
            return (CurrentStamina / MaximumStamina) * 100;
        }
    }

    public OnCurrentStaminaChanged OnCurrentStaminaChanged;


    public void AddToCurrentStamina(float value)
    {
        if (value == 0) return;

        float cachedCurrentStamina = CurrentStamina;

        if (value > 0)
        {
            GotHealedFor(value);
        }
        else
        {
            GotHitFor(damage: value);
        }

        OnCurrentStaminaChanged.Invoke(new CurrentStamina(cachedCurrentStamina, CurrentStamina, CurrentStaminaPercentage));
    }

    void GotHealedFor(float value)
    {
        CurrentStamina += value;

        if (CurrentStamina > MaximumStamina)
        {
            CurrentStamina = MaximumStamina;
        }
    }

    void GotHitFor(float damage)
    {
        float absoluteValue = Mathf.Abs(damage);
        DecreaseCurrentStaminaBy(absoluteValue);
    }

    void DecreaseCurrentStaminaBy(float value)
    {
        CurrentStamina -= value;

        if (CurrentStamina <= 0)
        {
            CurrentStamina = 0;
        }
        if(recharge != null) StopCoroutine(recharge);
        recharge = StartCoroutine(RechargeStamina());
    }

    private IEnumerator RechargeStamina(){
        yield return new WaitForSeconds(ChargeCooldown);

        while(CurrentStamina < MaximumStamina){
            AddToCurrentStamina(ChargeRate/10f);
            yield return new WaitForSeconds(0.1f);
        }
    }
}