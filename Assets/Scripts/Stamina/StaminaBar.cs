using System.Collections;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(FollowCameraRotation))]
public class StaminaBar : MonoBehaviour
{
    [SerializeField] bool isBillboarded = true;
    [SerializeField] bool shouldShowStaminaNumbers = true;

    float finalValue;
    float animationSpeed = 0.1f;
    float leftoverAmount = 0f;

    // Caches
    StaminaSystem StaminaSystem;
    public Image image_above;
    public Image image;
    Text text;
    FollowCameraRotation followCameraRotation;

    private void Start()
    {
        StaminaSystem = GetComponentInParent<StaminaSystem>();
        //image = GetComponentInChildren<Image>();
        text = GetComponentInChildren<Text>();
        followCameraRotation = GetComponent<FollowCameraRotation>();
        StaminaSystem.OnCurrentStaminaChanged.AddListener(ChangeStaminaFill);
    }

    void Update()
    {
        animationSpeed = StaminaSystem.AnimationDuration;

        if (!StaminaSystem.HasAnimationWhenStaminaChanges)
        {
            image.fillAmount = StaminaSystem.CurrentStaminaPercentage / 100;
        }

        text.text = $"{(int)StaminaSystem.CurrentStamina}/{StaminaSystem.MaximumStamina}";

        text.enabled = shouldShowStaminaNumbers;

        followCameraRotation.enabled = isBillboarded;
    }

    private void ChangeStaminaFill(CurrentStamina currentStamina)
    {
        if (!StaminaSystem.HasAnimationWhenStaminaChanges) return;
        if(currentStamina.previous > currentStamina.current)
            image_above.fillAmount = currentStamina.percentage / 100;
        StopAllCoroutines();
        StartCoroutine(ChangeFillAmount(currentStamina));
    }

    private IEnumerator ChangeFillAmount(CurrentStamina currentStamina)
    {
        finalValue = currentStamina.percentage / 100;

        float cacheLeftoverAmount = this.leftoverAmount;

        float timeElapsed = 0;

        while (timeElapsed < animationSpeed)
        {
            float leftoverAmount = Mathf.Lerp((currentStamina.previous / StaminaSystem.MaximumStamina) + cacheLeftoverAmount, finalValue, timeElapsed / animationSpeed);
            this.leftoverAmount = leftoverAmount - finalValue;
            if(currentStamina.previous <= currentStamina.current)
                image_above.fillAmount = leftoverAmount;
            image.fillAmount = leftoverAmount;
            timeElapsed += Time.deltaTime;
            yield return null;
        }

        this.leftoverAmount = 0;
        image_above.fillAmount = finalValue;
        image.fillAmount = finalValue;
    }
}