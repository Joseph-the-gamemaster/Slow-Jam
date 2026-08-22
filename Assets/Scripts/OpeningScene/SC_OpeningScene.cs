using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SC_OpeningScene : MonoBehaviour
{
    [Header("UI Overlay References")]
    [SerializeField] private GameObject vnOverlayCanvas; // Main Visual Novel UI Panel
    [SerializeField] private GameObject blinkFadeInObject; // 2.5-second blink/fade animation object
    [SerializeField] private GameObject Panel; // Panel for the VN sequence
    [SerializeField] private GameObject textBox;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private TMP_Text charNameText;
    [SerializeField] private GameObject nextButton;

    [Header("Character Sprites / Objects")]
    [SerializeField] private GameObject TimmothyObject;

    [Header("Player Control Reference")]
    [SerializeField] private PlayerMovementZ playerMovement; // Keeps player frozen during VN sequence

    private string textToSpeak;
    private int currentTextLength;
    private int eventPos = 0;

    void Start()
    {
        // Begin the visual novel sequence on start
        StartCoroutine(StartVNSequence());
    }

    IEnumerator StartVNSequence()
    {
        // 1. Freeze Player Movement & Enable VN Overlay UI
        if (playerMovement != null) playerMovement.SetMovementState(false);
        if (vnOverlayCanvas != null) vnOverlayCanvas.SetActive(true);

        // 2. Play 2.5 second Blink/Fade-In Animation
        if (blinkFadeInObject != null)
        {
            blinkFadeInObject.SetActive(true);
            yield return new WaitForSeconds(2.5f);
            blinkFadeInObject.SetActive(false);
        }

        // --- Event 0: Timmothy opening monologue ---
        if (TimmothyObject != null) TimmothyObject.SetActive(true);

        Panel.SetActive(true);
        textBox.SetActive(true);
        charNameText.text = "Timmothy";
        textToSpeak = "Huh?";
        dialogueText.text = textToSpeak;

        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitUntil(() => TextCreator.charCount >= currentTextLength);
        yield return new WaitForSeconds(0.5f);

        nextButton.SetActive(true);
        eventPos = 1;
    }

    IEnumerator EventOne()
    {
        // --- Event 1: Timmothy Continues ---
        nextButton.SetActive(false);

        charNameText.text = "Timmothy";
        textToSpeak = "Where am I? What is this place?";
        dialogueText.text = textToSpeak;

        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;

        yield return new WaitForSeconds(0.05f);
        yield return new WaitUntil(() => TextCreator.charCount >= currentTextLength);
        yield return new WaitForSeconds(0.5f);

        nextButton.SetActive(true);
        eventPos = 2;
    }

    IEnumerator EventTwo()
    {
        // --- Event 4: End VN sequence, close overlay UI, restore player control ---
        nextButton.SetActive(false);
        Panel.SetActive(false);
        textBox.SetActive(false);

        if (TimmothyObject != null) TimmothyObject.SetActive(false);
        if (vnOverlayCanvas != null) vnOverlayCanvas.SetActive(false);

        // Unfreeze Player Movement for overworld navigation
        if (playerMovement != null) playerMovement.SetMovementState(true);

        yield return null;
    }

    // Connect this to the Next Button's OnClick event in Inspector
    public void OnNextButtonClicked()
    {
        switch (eventPos)
        {
            case 1:
                StartCoroutine(EventOne());
                break;
            case 2:
                StartCoroutine(EventTwo());
                break;
        }
    }
}