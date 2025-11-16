using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter CuttingCounter;
    [SerializeField] private Image ProgressBarImage;

    private void Start()
    {
        ProgressBarImage.fillAmount = 0f;
        CuttingCounter.OnProgressChanged += CuttingCounter_OnProgressChanged;
        Hide();
    }

    private void CuttingCounter_OnProgressChanged(object sender, CuttingCounter.OnProgressChangedEventArgs e)
    {
        ProgressBarImage.fillAmount = e.ProgressNormalized;
        if (e.ProgressNormalized >= 1f)
        {
            Hide();
        }
        else
        {
            Show();
        }
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
