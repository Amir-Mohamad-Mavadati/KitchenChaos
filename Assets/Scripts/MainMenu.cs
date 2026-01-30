using UnityEngine;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField] private Button PlayButton;
    [SerializeField] private Button QuitButton;

    private void Awake()
    {
        PlayButton.onClick.AddListener(() =>
        {
            Loader.Load(Loader.SceneName.GameScene);
        });

        QuitButton.onClick.AddListener(() =>
        {
            Application.Quit();
        });

        Time.timeScale = 1f;
    }
}
