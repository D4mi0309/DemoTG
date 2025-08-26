using System.Collections;
using UnityEngine;
using TMPro;

public class DialogueController : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField, TextArea(1,4)] private string[] dialogueLines;

    [SerializeField] private float typingSpeed = 0.05f;
    [SerializeField] private bool startWithLine0OnStart = true;
    [SerializeField] private bool hidePanelWhenFinished = false;

    private Coroutine typingRoutine;
    private int currentLine = -1;

    private void Start()
    {
        if (dialogueLines == null || dialogueLines.Length == 0)
        {
            Debug.LogWarning("DialogueController: no hay líneas de diálogo asignadas.");
            if (dialoguePanel) dialoguePanel.SetActive(false);
            return;
        }

        if (startWithLine0OnStart)
        {
            ShowLineByIndex(0); // ← diálogo 1 al entrar en escena
        }
        else if (dialoguePanel)
        {
            dialoguePanel.SetActive(false);
        }
    }

    public void ShowLineByIndex(int index)
    {
        if (dialogueLines == null || index < 0 || index >= dialogueLines.Length)
        {
            Debug.LogWarning($"DialogueController: índice fuera de rango ({index}).");
            return;
        }

        if (typingRoutine != null) StopCoroutine(typingRoutine);

        currentLine = index;
        if (dialoguePanel) dialoguePanel.SetActive(true);
        dialogueText.text = string.Empty;
        typingRoutine = StartCoroutine(TypeLine(dialogueLines[index]));
    }

    public void ShowNext()
    {
        var nextIndex = currentLine + 1;
        if (nextIndex < dialogueLines.Length)
        {
            ShowLineByIndex(nextIndex);
        }
        else if (hidePanelWhenFinished && dialoguePanel)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private IEnumerator TypeLine(string line)
    {
        foreach (char c in line)
        {
            dialogueText.text += c;
            yield return new WaitForSeconds(typingSpeed);
        }
        typingRoutine = null;
    }
}
