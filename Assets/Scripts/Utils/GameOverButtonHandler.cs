using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonHandler : MonoBehaviour
{
    public void OnRestartButton()
    {
        Time.timeScale = 1f;



        // 수동으로 싱글톤 초기화
        if (GameManager.Instance != null)
        {
            GameManager.Instance.ResetState(); // 수동 초기화 함수
            GameManager.Instance?.Wave?.ResetState();
        }
       

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainTitle"); // 메인 메뉴 씬 이름
    }
}
