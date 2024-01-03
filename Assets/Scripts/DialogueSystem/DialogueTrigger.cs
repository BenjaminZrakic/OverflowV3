using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;

    private void Start() {
        dialogue.filePath = Application.dataPath + "/DialogueFiles/"+dialogue.dialogueFileName+".txt";
        dialogue.sentences = File.ReadAllLines(dialogue.filePath);
        /*foreach (string sentence in dialogue.sentences){
            Debug.Log(sentence);
        }*/
    }

    public void TriggerDialogue(){
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
    }
}
