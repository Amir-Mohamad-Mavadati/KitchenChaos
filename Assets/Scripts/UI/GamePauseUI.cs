using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button Resume;
    [SerializeField] private Button MainMenu;
    [SerializeField] private Button OptionSettings;
    [SerializeField] private Button Controls;
    [SerializeField] private ControlsUI ClassControlsUI;
    private void Start()
    {
        GameManager.Instance.OnPausedGame += GameManager_OnPausedGame;
        GameManager.Instance.OnUnPausedGame += GameManager_OnUnPausedGame;
        Hide();
    }

    private void Awake()
    {
        
        Resume.onClick.AddListener(() =>
        {
            Time.timeScale = 1f;
            Hide();
        });

        MainMenu.onClick.AddListener(() =>
        {
            Loader.Load(Loader.SceneName.MainMenuScene);
        });

        OptionSettings.onClick.AddListener(() =>
        {
            OptionUI.Instance.Show();
            Hide();
        });
        Controls.onClick.AddListener(() =>
        {
            Hide();
            ClassControlsUI.Show();
            
        });
    }

    private void GameManager_OnPausedGame(object Sender, System.EventArgs e)
    {
        Show();
    }

    private void GameManager_OnUnPausedGame(object Sender, System.EventArgs e)
    {
        Hide();
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
