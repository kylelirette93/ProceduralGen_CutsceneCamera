using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FadePanel : MonoBehaviour
{
    public Image canvasImage;
    public float fadeDuration = 1.5f;
    public CameraController cameraController;

    private void OnEnable()
    {
        cameraController = FindFirstObjectByType<CameraController>();
        if (cameraController != null) cameraController.FadeCanvas.AddListener(StartFade);
    }

    public void StartFade()
    {
        StartCoroutine(StartFadeToBlack());
    }
    public void ResetFade()
    {
        StartCoroutine(StartFadeToClear());
    }
    private IEnumerator StartFadeToBlack()
    {
        float alpha = 0f;
        float fadeSpeed = 1f / fadeDuration;
        Color color = canvasImage.color;
        while (alpha < 1f)
        {
            alpha += fadeSpeed * Time.deltaTime;
            canvasImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
        yield return new WaitForSeconds(fadeDuration - 1f);
        ResetFade();
        yield return new WaitForSeconds(fadeDuration + 1f);
        cameraController.hasFaded = false;
    }

    private IEnumerator StartFadeToClear()
    {
        float alpha = 1f;
        float fadeSpeed = 1f / fadeDuration;
        Color color = canvasImage.color;
        while (alpha > 0f)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            canvasImage.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }
    }
}
