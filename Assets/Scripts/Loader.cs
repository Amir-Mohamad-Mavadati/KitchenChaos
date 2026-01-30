using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{
    public enum SceneName
    {
        GameScene,
        MainMenuScene,
        LoadingScene,
    }

    private static SceneName TargetScene;

    public static void Load(SceneName TargetScene)
    {
        Loader.TargetScene = TargetScene;
        SceneManager.LoadScene(SceneName.LoadingScene.ToString());
    }

    public static void LoadingCallBack()
    {
        SceneManager.LoadScene(TargetScene.ToString());
    }
}
