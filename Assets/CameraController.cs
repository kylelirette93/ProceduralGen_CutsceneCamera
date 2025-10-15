using TreeEditor;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

public class CameraController : MonoBehaviour
{
    public Transform target;
    float currentPos = 0f;
    float cameraSpeed = 0.2f;
    SplineContainer container;

    private void OnEnable()
    {
        container = GameObject.Find("Spline").GetComponent<SplineContainer>();
    }


    private void LateUpdate()
    {
        currentPos += cameraSpeed * Time.deltaTime;
        Vector3 newPosition = (Vector3)container.EvaluatePosition(currentPos);
        currentPos = Mathf.Clamp(currentPos + cameraSpeed * Time.deltaTime, 0f, container.Spline.GetLength());
        transform.position = newPosition;
        transform.LookAt(target.position);
    }
}
