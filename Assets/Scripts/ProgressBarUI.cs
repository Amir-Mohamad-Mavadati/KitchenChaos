using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject HasProgressGameObj;
    [SerializeField] private Image ProgressBarImage;
    private IHasProgress HasProgress;

    private void Start()
    {
        HasProgress = HasProgressGameObj.GetComponent<IHasProgress>();

        if (HasProgressGameObj == null)
        {
            Debug.LogError("game object " + HasProgressGameObj + "does not have a component that implements IHasProgress!");
        }

        ProgressBarImage.fillAmount = 0f;

        HasProgress.OnProgressChanged += IHasProgress_OnProgressChanged;
        
        Hide();
    }

    private void IHasProgress_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        ProgressBarImage.fillAmount = e.ProgressNormalized;
        if (e.ProgressNormalized >= 1f || e.ProgressNormalized <= 0)
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
