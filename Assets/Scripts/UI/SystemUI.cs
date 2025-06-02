using UnityEngine;
using UnityEngine.SceneManagement;

public class SystemUI : MonoBehaviour
{
    public void StartGame(string sceneName)
    {
        GameManager.Instance.LoadNextStage(sceneName);
    }

    public void ExitGame()
    {
        GameManager.Instance.ExitGame();
    }

    public void LoadMenu(string menuSceneName)
    {
        GameManager.Instance.LoadNextStage(menuSceneName);
    }

    public void RestartScene()
    {
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }



}
