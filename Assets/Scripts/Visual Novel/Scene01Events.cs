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
        TextBox.SetActive(true);
        GirlGasp.Play();
        yield return new WaitForSeconds(2f);
        SideCharacter.SetActive(true);
        GirlSigh.Play();
    }
}
