using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableBook : Interactable
{
    public CollectedObjectEvent collectedObjectEvent;

    public override void Start()
    {
        base.Start();
    }

    protected override void Interaction()
    {
        base.Interaction();
        //print("Hello! I'm a flower.");

        //Set active particles
        //particleEffect.SetActive(true);
        interactableNameText.HideText();
        isInteractable = false;
        hasBeenActivated = true;
        collectedObjectEvent.Invoke();
        TargetOff();

        gameObject.SetActive(false);

    }
}
