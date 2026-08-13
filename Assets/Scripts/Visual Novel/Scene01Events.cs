using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scene01Events : MonoBehaviour
{
    public GameObject FadeIn;
    public GameObject MC;
    public GameObject SideCharacter;
    public GameObject TextBox;
    [SerializeField] AudioSource GirlSigh;
    [SerializeField] AudioSource GirlGasp;

        [SerializeField] string textToSpeak;
        [SerializeField] int currentTextLength;
        [SerializeField] int textLength;
        [SerializeField] GameObject mainTextObject;

        void Update()
        {
            textLength = TextCreator.charCount;
        }

    void Start()
    {
        StartCoroutine(EventStarter());
    }

    IEnumerator EventStarter()
    {
        yield return new WaitForSeconds(2f);
        FadeIn.SetActive(false);
        MC.SetActive(true);
        yield return new WaitForSeconds(2f);
        // this is where our text function will be called to display the text on the screen
        mainTextObject.SetActive(true);
        textToSpeak = "Haruka's late? Well that's a surprise!";
        TextBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        GirlGasp.Play();
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);


        TextBox.SetActive(true);
        yield return new WaitForSeconds(2f);
        SideCharacter.SetActive(true);
        GirlSigh.Play();
    }
}
