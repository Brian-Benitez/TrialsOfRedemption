using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public GameObject UIDialogueCanvas;
    public TextMeshProUGUI dialogueArea;

    private Queue<DialogueLine> lines;

    public bool isDialogueActive = false;
    public bool IsDialogueInProgress = false;
    public bool IsThereMoreDialogue = false;

    public float TypingSpeed;
    float amountOfLetters;

    public int SentencesCount = 0;

    private void Awake()
    {
        lines = new Queue<DialogueLine>();
    }


    public void StartDialogue(Dialogue dialogue)
    {
        AudioController.Instance.PlayTextSFX();
        isDialogueActive = true;
        TurnOnDialogueUI();
        lines.Clear();
        foreach (DialogueLine dialogueLine in dialogue.DialogueLines)
        {
            lines.Enqueue(dialogueLine);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine()
    {
        if (lines.Count == 0)
        {
            IsThereMoreDialogue = false;
            EndDialogue();
            return;
        }

        IsThereMoreDialogue = true;
        DialogueLine currentLine = lines.Dequeue();
        StopAllCoroutines();
        AudioController.Instance.PlayTextSFX();
        StartCoroutine(TypeSentence(currentLine));
    }

    IEnumerator TypeSentence(DialogueLine dialogueLine)
    {
        SentencesCount++;
        dialogueArea.text = "";

        foreach (char letter in dialogueLine.line.ToCharArray())
        {
            amountOfLetters++;
            IsDialogueInProgress = true;
            dialogueArea.text += letter;
            yield return new WaitForSeconds(TypingSpeed);
        }

        yield return new WaitForSecondsRealtime(amountOfLetters * 0.05f - 4f);
        Debug.Log("how long are we waitin " + amountOfLetters * 0.05f);
        AudioController.Instance.StopTextSFX();
        amountOfLetters = 0;
        IsDialogueInProgress = false;
    }


    public void TurnOnDialogueUI()
    {
        UIDialogueCanvas.SetActive(true);
    }
    public void TurnOffDialogueUI()
    {
        if( UIDialogueCanvas != null )
            UIDialogueCanvas.SetActive(false);

        AudioController.Instance.StopTextSFX();
    }
    void EndDialogue()
    {
        SentencesCount = 0;
        isDialogueActive = false;
        UIDialogueCanvas.SetActive(false);
    }
}
