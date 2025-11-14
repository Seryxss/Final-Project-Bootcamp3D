using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    public CanvasGroup group;
    public float duration = 0.5f;

    // Fade in (hitam muncul)
    public void FadeIn(System.Action onComplete = null)
    {
        LeanTween.alphaCanvas(group, 1f, duration).setOnComplete(() =>
        {
            if (onComplete != null) onComplete();
        });
    }

    // Fade out (hitam hilang)
    public void FadeOut(System.Action onComplete = null)
    {
        LeanTween.alphaCanvas(group, 0f, duration).setOnComplete(() =>
        {
            if (onComplete != null) onComplete();
        });
    }
}
