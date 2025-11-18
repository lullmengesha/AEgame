using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class SimpleLoadingBar : MonoBehaviour
{
    public Image fillImage;
    public float fillDuration = 2f;

    private Tween fillTween;

    void Start()
    {
        fillImage.fillAmount = 0f;
    }

    // Call this to fill the bar
    public void FillBar(System.Action onComplete = null)
    {
        fillTween?.Kill();

        fillTween = fillImage.DOFillAmount(1f, fillDuration)
            .OnComplete(() => {
                onComplete?.Invoke();
            });
    }

    // Reset bar to empty
    public void ResetBar()
    {
        fillTween?.Kill();
        fillImage.fillAmount = 0.448f;
    }
}