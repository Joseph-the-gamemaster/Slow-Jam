using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EncounterTransitionZ : MonoBehaviour
{
    [Header("Scene Settings")]
    [SerializeField] private string combatSceneName = "BattleScene";

    [Header("Effect References")]
    [SerializeField] private Camera mainCamera;
    [SerializeField] private Image flashOverlay; // A UI Panel covering the screen (white/black color)
    [SerializeField] private CameraZoomZ cameraZoom;

    [Header("Animation Settings")]
    [SerializeField] private float zoomOutScale = 7f;
    [SerializeField] private float zoomInScale = 2f;
    [SerializeField] private float transitionDuration = 1.2f;
    [SerializeField] private int flashCount = 4;

    private bool isTransitioning = false;

    void Awake()
    {
        if (mainCamera == null)
            mainCamera = Camera.main;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Trigger encounter if overlapping player
        if (other.CompareTag("Player") && !isTransitioning)
        {
            StartCoroutine(StartEncounterRoutine(other.gameObject));
        }
    }

    private IEnumerator StartEncounterRoutine(GameObject player)
    {
        isTransitioning = true;

        // Freeze player movement during transition
        PlayerMovementZ playerMovement = player.GetComponent<PlayerMovementZ>();
        if (playerMovement != null)
        {
            playerMovement.SetMovementState(false);
        }

        float phaseTime = transitionDuration / 2f;

        // Phase 1: Zoom Out using Cinemachine
        if (cameraZoom != null)
        {
            cameraZoom.ZoomTo(zoomOutScale, phaseTime);
        }
        yield return new WaitForSeconds(phaseTime);

        // Phase 2: Screen Flashing
        if (flashOverlay != null)
        {
            flashOverlay.gameObject.SetActive(true);
            for (int i = 0; i < flashCount; i++)
            {
                flashOverlay.color = new Color(1, 1, 1, 1); // White flash
                yield return new WaitForSeconds(0.06f);
                flashOverlay.color = new Color(0, 0, 0, 0); // Transparent
                yield return new WaitForSeconds(0.06f);
            }
        }

        // Phase 3: Zoom In rapidly before loading scene
        if (cameraZoom != null)
        {
            cameraZoom.ZoomTo(zoomInScale, phaseTime);
        }
        yield return new WaitForSeconds(phaseTime);

        // Load Turn-Based Battle Scene
        SceneManager.LoadScene(combatSceneName);
    }
}