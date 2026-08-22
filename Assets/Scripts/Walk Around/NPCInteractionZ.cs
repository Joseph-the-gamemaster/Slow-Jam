using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteractionZ : MonoBehaviour
{
    [Header("Visual Novel Scene")]
    [SerializeField] private string visualNovelSceneName = "VNScene";

    [Header("UI Indicator")]
    [SerializeField] private GameObject interactionPrompt; // Assign InteractionCanvas here

    void Start()
    {
        // Ensure prompt is hidden at startup
        if (interactionPrompt != null)
        {
            interactionPrompt.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Show prompt when player enters interaction trigger
        if (other.CompareTag("Player"))
        {
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(true);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        // Hide prompt when player walks away
        if (other.CompareTag("Player"))
        {
            if (interactionPrompt != null)
            {
                interactionPrompt.SetActive(false);
            }
        }
    }

    public void TriggerDialogueScene()
    {
        Debug.Log("Interacted with NPC! Transitioning to Visual Novel scene...");
        SceneManager.LoadScene(visualNovelSceneName);
    }
}