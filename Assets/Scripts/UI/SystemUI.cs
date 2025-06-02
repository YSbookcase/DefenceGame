using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemUI : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private Slider volumeSlider;

    private bool isMenuOpen = false;


    private void Awake()
    {
        // menuPanel이 미리 지정되지 않은 경우, 태그나 이름으로 찾아 할당
        if (menuPanel == null)
        {
            GameObject found = GameObject.FindWithTag("Menu"); // 또는 "MenuPanel" 이름 사용
            if (found != null)
            {
                menuPanel = found;
            }
            else
            {
                //Debug.LogWarning("Menu Panel을 찾을 수 없습니다. 이 씬에는 메뉴가 없을 수 있습니다.");
            }
        }
    }


    private void Start()
    {

        if (volumeSlider != null)
        {
            float initialVolume = GameManager.Instance.Audio.GetBgmVolume();
            volumeSlider.value = initialVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        if (menuPanel != null)
            CloseMenu(); // null일 때는 아무 것도 안 함
        else
        {
            //Debug.LogWarning("SystemUI: menuPanel이 설정되지 않았습니다. 이 씬에는 메뉴가 없을 수 있습니다.");
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuPanel != null)
        {
            ToggleMenu();
        }
    }


    public void StartGame(string sceneName)
    {
        ResumeGame(); // 씬 이동 전 시간 재개
        GameManager.Instance.LoadScene(sceneName);
    }

    public void ExitGame()
    {
        ResumeGame();
        GameManager.Instance.ExitGame();
    }

    public void LoadScene(string sceneName)
    {
        ResumeGame();
        GameManager.Instance.LoadScene(sceneName);
    }

    public void RestartScene()
    {
        ResumeGame();
        string currentScene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentScene);
    }

    public void ToggleMenu()
    {
        if (isMenuOpen)
            CloseMenu();
        else
            OpenMenu();
    }

    public void OpenMenu()
    {
        if (menuPanel == null) return;

        isMenuOpen = true;
        menuPanel?.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        if (menuPanel == null) return;

        isMenuOpen = false;
        menuPanel?.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }
    private void OnVolumeChanged(float value)
    {
        GameManager.Instance.Audio.SetBgmVolume(value);
    }

 
}
