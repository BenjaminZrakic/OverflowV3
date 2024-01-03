using System.Collections;
using System.Collections.Generic;
using UnityEngine;
 
public class InteractableNPC : Interactable
{
    private Animator animator;
    private DialogueTrigger dialogueTrigger;
    public override void Start()
    {
        base.Start();
        dialogueTrigger = GetComponent<DialogueTrigger>();
        //animator = GetComponent<Animator>();
    }
    protected override void Interaction()
    {
        base.Interaction();
        dialogueTrigger.TriggerDialogue();
        //animator.SetTrigger("Wave");
 
        //Start Dialogue System
    
    }
}