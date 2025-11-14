using UnityEngine;

public class FadeCanvas : MonoBehaviour
{
    public CanvasGroup group;
    public float duration = 0.5f;

    public void FadeIn(System.Action onComplete = null)
    {
        LeanTween.alphaCanvas(group, 1f, duration).setOnComplete(() =>
        {
            if (onComplete != null) onComplete();
        });
    }

    public void FadeOut(System.Action onComplete = null)
    {
        LeanTween.alphaCanvas(group, 0f, duration).setOnComplete(() =>
        {
            if (onComplete != null) onComplete();
        });
    }
}