using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleUI : MonoBehaviour
{
    [SerializeField] private Button startButton;
    [SerializeField] private Button exitButton;

    private void Start()
    {
        startButton.onClick.AddListener(() => SceneManager.LoadScene("MainGame"));
        exitButton.onClick.AddListener(() => GameManager.Instance.ExitGame());
    }
}