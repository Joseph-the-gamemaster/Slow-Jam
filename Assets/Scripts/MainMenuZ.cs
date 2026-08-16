using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuZ : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string firstSceneName = "Overworld"; // Name of the scene you want to load

    // Call this method when the Start Button is clicked
    public void PlayGame()
    {
        Debug.Log("Loading first scene...");
        SceneManager.LoadScene(firstSceneName);
    }

    // Optional: Call this method if you have a Quit Button
    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}