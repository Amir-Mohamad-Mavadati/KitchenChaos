using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI MoveUpText;
    [SerializeField] private TextMeshProUGUI MoveDownText;
    [SerializeField] private TextMeshProUGUI MoveRightText;
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    [SerializeField] private TextMeshProUGUI IntractText;
    [SerializeField] private TextMeshProUGUI AlternateText;
    [SerializeField] private TextMeshProUGUI PauseText;

    private void Start()
    {
        UpdateVisual();
        Show();
        PlayerInput.Instance.OnBindingRebind += PlayerInput_OnBindingRebind;
        GameManager.Instance.OnStateChanged += GameManager_OnStateChanged;
    }

    private void GameManager_OnStateChanged(object Sender, System.EventArgs e)
    {
        if(GameManager.Instance.IsCountDownActive())
        {
            Hide();
        }
        
    }

    private void PlayerInput_OnBindingRebind(object Sender, System.EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        MoveUpText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Up);
        MoveDownText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Down);
        MoveRightText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Right);
        MoveLeftText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Move_Left);
        IntractText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Intract);
        AlternateText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Intract_Alternate);
        PauseText.text = PlayerInput.Instance.GetBindingText(PlayerInput.Binding.Pause);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
