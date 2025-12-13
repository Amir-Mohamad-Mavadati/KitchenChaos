using TMPro;
using Unity.Mathematics;
using UnityEngine;

public class CountDownTimerUI : MonoBehaviour
{
    private TextMeshProUGUI TextMeshPro;
    private void Start()
    {
        TextMeshPro = GetComponentInChildren<TextMeshProUGUI>();
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
        Hide();
    }

    private void GameManager_OnStateChanged(object Sender, System.EventArgs e)
    {
        if (GameManager.Instance.IsCountDownActive())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        TextMeshPro.text = math.ceil(GameManager.Instance.GetCountDownTimer()).ToString();
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
