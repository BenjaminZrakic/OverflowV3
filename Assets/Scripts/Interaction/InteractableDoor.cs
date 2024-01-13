using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableDoor : Interactable
{
    protected override void Interaction()
    {
        base.Interaction();
        //print("Hello! I'm a flower.");

        //Set active particles
        //particleEffect.SetActive(true);
        interactableNameText.HideText();
        isInteractable = false;
        hasBeenActivated = true;
        TargetOff();

        GetComponent<Animator>().SetTrigger("open_door");

    }
}
