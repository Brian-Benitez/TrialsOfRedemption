using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class DialogueLine
{
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialogue
{
    public List<DialogueLine> DialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    [Header("NOTE: Make sure you add the Dialouge Manager to this object!")]
    [Header("Booleans")]
    public bool RestartDialouge = true;
    public GameObject EKeyGameObject;

    [Header("Buttons")]
    public KeyCode InteractKeyCode = KeyCode.E;

    [Header("Dialouges lines")]
    public Dialogue Dialogue;

    private DialogueManager _dialogueManagerRef;

    private void Start()
    {
        _dialogueManagerRef = GetComponent<DialogueManager>();
    }

    public void TriggerDialogue()
    {
        _dialogueManagerRef.StartDialogue(Dialogue);
    }
    private void Update()
    {
        if(!RestartDialouge && Input.GetKeyUp(InteractKeyCode))
            return;
        if(_dialogueManagerRef.isDialogueActive && Input.GetKeyUp(InteractKeyCode))
        {
            RestartDialouge = false;
            Debug.Log("start dialouge");
            TriggerDialogue();
        }
        if(_dialogueManagerRef.IsThereMoreDialogue && Input.GetKeyUp(KeyCode.Space))
        {
            _dialogueManagerRef.DisplayNextDialogueLine();
            Debug.Log("am i here");
        }
    }


    void CheckList()
    {
        if (Dialogue.DialogueLines.Count == _dialogueManagerRef.SentencesCount && _dialogueManagerRef.isDialogueActive)
        {
            RestartDialouge = true;
            _dialogueManagerRef.SentencesCount = 0;
        }
        else if(_dialogueManagerRef.SentencesCount < Dialogue.DialogueLines.Count)
            RestartDialouge = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _dialogueManagerRef.isDialogueActive = true;
            EKeyGameObject.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            RestartDialouge = true;
            EKeyGameObject.SetActive(false);
            _dialogueManagerRef.isDialogueActive = false;
            _dialogueManagerRef.TurnOffDialogueUI();
        }
    }
}