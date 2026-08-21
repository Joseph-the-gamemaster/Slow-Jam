using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    [SerializeField] GameObject nextButton;
    [SerializeField] int eventPos = 0;
    [SerializeField] GameObject CharName;
    [SerializeField] GameObject FadeOut;

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
        // event 0
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
        nextButton.SetActive(true);
        eventPos = 1;
    }

    IEnumerator EventOne()
    {
        //event 1
        nextButton.SetActive(false);
        SideCharacter.SetActive(true);
        TextBox.SetActive(true);
        CharName.GetComponent<TMPro.TMP_Text>().text = "Haruka";
        textToSpeak = "What do you mean, I'm right here dawg!";
        TextBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        GirlSigh.Play();
        nextButton.SetActive(true);
        eventPos = 2;
    }

    IEnumerator EventTwo()
    {
        //event 2
        nextButton.SetActive(false);
        SideCharacter.SetActive(true);
        TextBox.SetActive(true);
        CharName.GetComponent<TMPro.TMP_Text>().text = "Kasumi";
        textToSpeak = "Sorry, I'm just a baka, te he!";
        TextBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 3;
    }

    IEnumerator EventThree()
    {
        //event 3
        nextButton.SetActive(false);
        SideCharacter.SetActive(true);
        TextBox.SetActive(true);
        CharName.GetComponent<TMPro.TMP_Text>().text = "Haruka";
        textToSpeak = "Grow Up, and let's get find Akane!";
        TextBox.GetComponent<TMPro.TMP_Text>().text = textToSpeak;
        currentTextLength = textToSpeak.Length;
        TextCreator.runTextPrint = true;
        yield return new WaitForSeconds(0.05f);
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil(() => textLength == currentTextLength);
        yield return new WaitForSeconds(0.5f);
        nextButton.SetActive(true);
        eventPos = 4;
    }

    IEnumerator EventFour()
    {
        //event 4
        nextButton.SetActive(false);
        SideCharacter.SetActive(true);
        TextBox.SetActive(true);
        FadeOut.SetActive(true);
        yield return new WaitForSeconds(2f);
        eventPos = 5;
        SceneManager.LoadScene("ParkScene01");
    }

    public void NextButton()
    {
        if (eventPos == 1)
        {
            StartCoroutine(EventOne());
        }
        else if (eventPos == 2)
        {
            StartCoroutine(EventTwo());
        }
        else if (eventPos == 3)
        {
            StartCoroutine(EventThree());
        }
        else if (eventPos == 4)
        {
            StartCoroutine(EventFour());
        }
        else if (eventPos == 5)
        {
            // Load next scene or perform next action
            Debug.Log("End of events. Proceed to next scene.");
        }
    }


}
