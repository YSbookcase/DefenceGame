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
        // menuPanel�� �̸� �������� ���� ���, �±׳� �̸����� ã�� �Ҵ�
        if (menuPanel == null)
        {
            GameObject found = GameObject.FindWithTag("Menu"); // �Ǵ� "MenuPanel" �̸� ���
            if (found != null)
            {
                menuPanel = found;
            }
            else
            {
                //Debug.LogWarning("Menu Panel�� ã�� �� �����ϴ�. �� ������ �޴��� ���� �� �ֽ��ϴ�.");
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
            CloseMenu(); // null�� ���� �ƹ� �͵� �� ��
        else
        {
            //Debug.LogWarning("SystemUI: menuPanel�� �������� �ʾҽ��ϴ�. �� ������ �޴��� ���� �� �ֽ��ϴ�.");
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
        ResumeGame(); // �� �̵� �� �ð� �簳
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
