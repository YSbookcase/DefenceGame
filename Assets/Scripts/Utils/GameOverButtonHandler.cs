using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverButtonHandler : MonoBehaviour
{
    public void OnRestartButton()
    {
        Time.timeScale = 1f;



        // �������� �̱��� �ʱ�ȭ


        Destroy(gameObject);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OnQuitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainTitle"); // ���� �޴� �� �̸�
    }
}
