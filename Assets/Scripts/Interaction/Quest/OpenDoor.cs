using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenDoor : MonoBehaviour
{
    public Interactable[] objectsToCollect;
    public GameObject doorToOpen;


    public void checkIfAllObjectsCollected(){
        print("Checking objects");
        foreach (Interactable objectToCheck in objectsToCollect){
            if (objectToCheck.hasBeenActivated == false){
                return;
            }
        }
        doorToOpen.SetActive(false);
    }
}
