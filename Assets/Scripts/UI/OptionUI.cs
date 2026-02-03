using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class OptionUI : MonoBehaviour
{
    public static OptionUI Instance { get; private set; }
    [SerializeField] private Button MusicLevel;
    [SerializeField] private Button SoundEffectLevel;
    [SerializeField] private Button Close;
    [SerializeField] private GamePauseUI ClassGamePauseUI;
    [SerializeField] private TextMeshProUGUI MusicText;
    [SerializeField] private TextMeshProUGUI SoundEffectText;
    [SerializeField] private MusicManager ClassMusicManager;
    

    private void Awake()
    {
        Instance = this;
        MusicLevel.onClick.AddListener(() =>
        {
            ClassMusicManager.ChangeMusicVolume();
            UpdateVisual();
        });
        SoundEffectLevel.onClick.AddListener(() =>
        {
            SoundManager.Instance.ChangeSoundEffectsVolume();
            UpdateVisual();
        });
        Close.onClick.AddListener(() =>
        {
            Hide();
            ClassGamePauseUI.Show();
        });
        
    }

    private void Start()
    {
        UpdateVisual();
        Hide();
    }

    private void UpdateVisual()
    {
        MusicText.text = "Music: " + Mathf.Round(ClassMusicManager.GetMusicVolume() * 10f);
        SoundEffectText.text = "Sound Effect: " + Mathf.Round(SoundManager.Instance.GetSoundEffectVolume() * 10f);
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
