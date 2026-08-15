using System.Collections;
using UnityEngine;
using Cinemachine;

public class CameraZoomZ : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera vcam;

    void Awake()
    {
        if (vcam == null)
            vcam = GetComponent<CinemachineVirtualCamera>();
    }

    // Call this function to safely zoom over time
    public void ZoomTo(float targetSize, float duration)
    {
        // Clamp targetSize so it can NEVER be 0 or negative
        targetSize = Mathf.Max(0.1f, targetSize); 
        StartCoroutine(SmoothZoomRoutine(targetSize, duration));
    }

    private IEnumerator SmoothZoomRoutine(float targetSize, float duration)
    {
        float startSize = vcam.m_Lens.OrthographicSize;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            vcam.m_Lens.OrthographicSize = Mathf.Lerp(startSize, targetSize, elapsed / duration);
            yield return null;
        }

        vcam.m_Lens.OrthographicSize = targetSize;
    }
}