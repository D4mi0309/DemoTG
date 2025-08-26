using System.Collections;
using UnityEngine;
using TMPro;

public class Dialogue : MonoBehaviour
{
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text DialogueText;
    [SerializeField, TextArea(1, 4)] private string[] dialogueLines;

    [SerializeField] private float typingSpeed = 0.05f;   // Velocidad de tipeo
    [SerializeField] private float lineDelay = 1.8f;      // Tiempo de espera entre frases

    private int currentLine;

    private void Start()
    {
        dialoguePanel.SetActive(true);  // Mostrar el panel al inicio
        currentLine = 0;
        StartCoroutine(ShowLine());
    }

    private IEnumerator ShowLine()
    {
        DialogueText.text = string.Empty;

        // Escribir letra por letra
        foreach (char letter in dialogueLines[currentLine])
        {
            DialogueText.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }

        // Esperar un tiempo antes de pasar a la siguiente línea
        yield return new WaitForSeconds(lineDelay);

        currentLine++;

        if (currentLine < dialogueLines.Length)
        {
            StartCoroutine(ShowLine());
        }
        /*
        else
        {
            dialoguePanel.SetActive(false); // Oculta el panel al terminar
        }
        */
    }
}
