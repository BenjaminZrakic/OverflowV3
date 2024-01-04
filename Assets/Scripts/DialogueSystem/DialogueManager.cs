using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public Queue<string> sentences;
    InputAction continueDialogueAction;
    public TMP_Text nameText;
    public TMP_Text dialogueText;

    public GameObject dialogueBox;
    public GameObject player;

    InputAction interactAction;

    Animator dialogueAnimator;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueAnimator = dialogueBox.GetComponent<Animator>();
        //interactAction = GetComponent<PlayerInput>().actions["Interact"];
        

    }

    public void StartDialogue (Dialogue dialogue){

        player.GetComponentInChildren<Interactor>().enabled = false;
        player.GetComponent<PlayerControllerSimple>().enabled = false;

        print("Starting conversation with "+dialogue.name);
        dialogueAnimator.SetTrigger("open_dialogue");

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
        //interactAction.performed += DisplayNextSentence;
    }

    public void DisplayNextSentence(){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    public void DisplayNextSentence(InputAction.CallbackContext obj){
        if(sentences.Count == 0){
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        //Debug.Log(sentence);
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence (string sentence){
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray()){
            dialogueText.text+=letter;
            yield return null;
        }
    }
    void EndDialogue(){
        //interactAction.performed -= DisplayNextSentence;
        player.GetComponentInChildren<Interactor>().enabled = true;
        player.GetComponent<PlayerControllerSimple>().enabled = true;
        dialogueAnimator.SetTrigger("close_dialogue");
        Debug.Log("End of conversation");
    }
}

