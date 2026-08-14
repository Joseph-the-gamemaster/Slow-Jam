using UnityEngine;
using UnityEngine.SceneManagement;

public class NPCInteractionZ : MonoBehaviour
{
    [Header("Visual Novel Scene")]
    [SerializeField] private string visualNovelSceneName = "VNScene";

    public void TriggerDialogueScene()
    {
        Debug.Log("Interacted with NPC! Transitioning to Visual Novel scene...");
        SceneManager.LoadScene(visualNovelSceneName);
    }
}