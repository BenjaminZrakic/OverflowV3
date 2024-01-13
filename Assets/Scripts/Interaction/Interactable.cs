using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
	[Header("Interaction Data")]
	public string interactableName="";
	public float interactionDistance = 2;
	[SerializeField] public bool isInteractable = true;

	protected InteractableNameText interactableNameText;
	GameObject interactableNameCanvas;

	public bool hasBeenActivated = false;

	public int text_height=1;

	public virtual void Start()
	{
		interactableNameCanvas = GameObject.FindGameObjectWithTag("Canvas");
		interactableNameText = interactableNameCanvas.GetComponentInChildren<InteractableNameText>();
	}

	public void TargetOn()
	{
		//print("Found interactable, showing UI");
		if (isInteractable && this!=null && interactableNameText!=null){
			interactableNameText.ShowText(this);
        	//interactableNameText.SetInteractableNamePosition(this);
		}
        
	}

	public void TargetOff()
	{
		//print("No interactables found");
        interactableNameText.HideText();
    }

	public void Interact()
	{
		if (isInteractable) Interaction();
	}

	protected virtual void Interaction()
	{
        print("interact with: " + this.name);
		
    }

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.blue;
		Gizmos.DrawWireSphere(transform.position,interactionDistance);
	}
	private void OnDestroy()
	{
		TargetOff();
    }
}
