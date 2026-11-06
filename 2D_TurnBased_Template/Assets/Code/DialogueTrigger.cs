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
    public bool FinishedFirstDialouge = false;
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
        if(!RestartDialouge && Input.GetKeyUp(InteractKeyCode) || _dialogueManagerRef.IsDialogueInProgress)
            return;
        
        if(Input.GetKeyDown(InteractKeyCode))
        {
            if (FinishedFirstDialouge && _dialogueManagerRef.IsThereMoreDialogue)
            {
                Debug.Log("look hoe");
                _dialogueManagerRef.DisplayNextDialogueLine();
            }
            else if (_dialogueManagerRef.isDialogueActive)
            {
                TriggerDialogue();
                RestartDialouge = false;
                FinishedFirstDialouge = true;
                Debug.Log("cuck2");
            }
            Debug.Log("cuck");
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
            AudioController.Instance.IsPlayingTextSFX = false;
            RestartDialouge = true;
            EKeyGameObject.SetActive(false);
            _dialogueManagerRef.isDialogueActive = false;
            _dialogueManagerRef.TurnOffDialogueUI();
        }
    }
}