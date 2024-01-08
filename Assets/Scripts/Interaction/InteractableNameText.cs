using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
 
public class InteractableNameText : MonoBehaviour
{
    TextMeshProUGUI text;
 
    Transform cameraTransform;
    void Start()
    {
        text = GetComponentInChildren<TextMeshProUGUI>();
        cameraTransform = Camera.main.transform;
        HideText();
    }
    public void ShowText(Interactable interactable)
    {
        if(interactable is InteractableNPC)
        {
            text.text = interactable.interactableName + "\n [E] Speak";
        }
        else if(interactable is InteractableFlower)
        {
            text.text = interactable.interactableName + "\n [E] Touch flower";
        }
        else if(interactable is InteractableBook)
        {
            text.text = interactable.interactableName + "\n [E] Pickup book";
        }
        else if(interactable is Checkpoint)
        {
            text.text = interactable.interactableName + "\n [E] Activate totem";
        }
        else
        {
            text.text = interactable.interactableName;
        }
    }
 
    public void HideText()
    {
        text.text = "";
    }
 
    public void SetInteractableNamePosition(Interactable interactable)
    {
        if (interactable.TryGetComponent(out BoxCollider boxCollider))
        {
            //transform.position = interactable.transform.position + Vector3.up * boxCollider.bounds.size.y;
            transform.position = interactable.transform.position + Vector3.up * interactable.text_height;
            transform.LookAt(2 * transform.position - cameraTransform.position);
        }
        else if(interactable.TryGetComponent(out CapsuleCollider capsCollider))
        {

            //transform.position = interactable.transform.position + Vector3.up * capsCollider.height*2;
            transform.position = interactable.transform.position + Vector3.up * interactable.text_height;
            transform.LookAt(2 * transform.position - cameraTransform.position);
        }
        else
        {
            print("Error, no collider found!");
        }
      
 
    }
}