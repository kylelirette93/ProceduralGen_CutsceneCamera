using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

public class CameraController : MonoBehaviour
{
    public Transform target;

    [Header("Spline Settings")]
    public float transitionSpeed = 1f;
    public float autoRotateSpeed = 0.05f;
    SplineContainer container;

    // Spline traversal.
    float currentT = 0f;
    float targetT = 0f;
    float endT = 1f;
    float fadeThreshold = 0.8f;
    float distanceToFade = 0.1f;
    public bool hasFaded = false;

    public UnityEvent FadeCanvas;

    private void OnEnable()
    {
        container = GameObject.Find("Spline").GetComponent<SplineContainer>();
    }


    private void LateUpdate()
    {
        if (container == null || target == null) return;

        // Check if target has moved along spline.
        if (targetT < endT)
        {
            targetT += autoRotateSpeed * Time.deltaTime;
            targetT = Mathf.Clamp01(targetT);
        }

        int totalKnots = container.Spline.Count;

        // Gets the current knot index based on current t value.
        int currentKnotIndex = Mathf.FloorToInt(currentT * (totalKnots - 1));

        if (!hasFaded && Mathf.Abs(currentT - currentKnotIndex) > distanceToFade && currentT < fadeThreshold)
        {
            hasFaded = true;
            FadeCanvas?.Invoke();
        }

        // Lerp towards cars position.
        currentT = Mathf.Lerp(currentT, targetT, transitionSpeed * Time.deltaTime);

        // Evaluate spline at normalized position.
        Vector3 newPosition = container.EvaluatePosition(currentT);

        // Update position and look at.
        transform.position = newPosition;
        transform.LookAt(target.position);
    }
}
