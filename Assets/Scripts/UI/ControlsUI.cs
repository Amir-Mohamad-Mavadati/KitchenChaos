using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ControlsUI : MonoBehaviour
{

    [SerializeField] private Button MoveUpButton;
    [SerializeField] private Button MoveDownButton;
    [SerializeField] private Button MoveRightButton;
    [SerializeField] private Button MoveLeftButton;
    [SerializeField] private Button IntractButton;
    [SerializeField] private Button AlternateButton;
    [SerializeField] private Button PauseButton;
    [SerializeField] private Button CloseButton;
    [SerializeField] private TextMeshProUGUI MoveUpText;
    [SerializeField] private TextMeshProUGUI MoveDownText;
    [SerializeField] private TextMeshProUGUI MoveRightText;
    [SerializeField] private TextMeshProUGUI MoveLeftText;
    [SerializeField] private TextMeshProUGUI IntractText;
    [SerializeField] private TextMeshProUGUI AlternateText;
    [SerializeField] private TextMeshProUGUI PauseText;
    [SerializeField] private GamePauseUI ClassGamePauseUI;
    [SerializeField] private RebindingUI ClassRebindingUI;


    private void Awake()
    {
        MoveUpButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Move_Up);
        });
        MoveDownButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Move_Down);
        });
        MoveRightButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Move_Right);
        });
        MoveLeftButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Move_Left);
        });
        IntractButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Intract);
        });
        AlternateButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Intract_Alternate);
        });
        PauseButton.onClick.AddListener(() =>
        {
            RebindBinding(PlayerInput.Binding.Pause);
        });
        CloseButton.onClick.AddListener(() =>
        {
            Hide();
            ClassGamePauseUI.Show();
        });
        
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
    private void Start()
    {
        UpdateVisual();
        Hide();
    }
    private void RebindBinding(PlayerInput.Binding binding)
    {
        ClassRebindingUI.Show();
        PlayerInput.Instance.RebindBinding(binding, () =>
        {
            ClassRebindingUI.Hide();
            UpdateVisual();
        });
    }
    public void Show()
    {
        gameObject.SetActive(true);
    }
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
