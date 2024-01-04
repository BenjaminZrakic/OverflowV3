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

    InputAction interactAction;

    Animator dialogueAnimator;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
        dialogueAnimator = dialogueBox.GetComponent<Animator>();
        interactAction = GetComponent<PlayerInput>().actions["Interact"];
        interactAction.performed += DisplayNextSentence;
    }

    public void StartDialogue (Dialogue dialogue){

        print("Starting conversation with "+dialogue.name);
        dialogueAnimator.SetTrigger("open_dialogue");

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach (string sentence in dialogue.sentences){
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
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
        dialogueAnimator.SetTrigger("close_dialogue");
        Debug.Log("End of conversation");
    }
}

