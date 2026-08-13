using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextCreator : MonoBehaviour
{
    public static TMP_Text viewText;
    public static bool runTextPrint;
    public static int charCount;

    [SerializeField] private TMP_Text myTextComponent; // Assign or grab automatically
    [SerializeField] private string transferText;
    [SerializeField] private int internalCount;

    void Awake()
    {
        // Automatically get the component on this GameObject
        myTextComponent = GetComponent<TMP_Text>();

        // If viewText hasn't been set globally yet, default to this object's component
        if (viewText == null)
        {
            viewText = myTextComponent;
        }
    }

    void Update()
    {
        // 1. Safety check: make sure the text component actually exists on this object
        if (myTextComponent != null && myTextComponent.text != null)
        {
            internalCount = charCount;
            charCount = myTextComponent.text.Length;
        }

        // 2. Run the typewriter effect when triggered
        if (runTextPrint)
        {
            runTextPrint = false;

            if (viewText != null && myTextComponent != null)
            {
                transferText = myTextComponent.text;
                viewText.text = "";
                StartCoroutine(RollText());
            }
            else
            {
                Debug.LogError("TextCreator Error: 'viewText' or 'myTextComponent' is not assigned!");
            }
        }
    }

    IEnumerator RollText()
    {
        foreach (char c in transferText)
        {
            if (viewText != null)
            {
                viewText.text += c;
            }
            yield return new WaitForSeconds(0.03f);
        }
    }
}