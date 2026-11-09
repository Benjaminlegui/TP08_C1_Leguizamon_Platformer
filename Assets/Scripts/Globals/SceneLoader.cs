using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public const string MainMenuScene = "MainMenu";
    public const string GameplayScene = "SampleScene";

    public static void LoadMainMenu() => Load(MainMenuScene);
    public static void LoadGameplay() => Load(GameplayScene);

    private static void Load(string sceneName)
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(sceneName);
    }
}