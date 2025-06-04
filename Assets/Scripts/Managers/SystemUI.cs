using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DesignPattern;

public class SystemUI : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject victoryPanel;
    [SerializeField] private Slider volumeSlider;

    [SerializeField] private SunRainManager sunRainManager;
    [SerializeField] private UnitCardSelectionManager selectionManager;

    [SerializeField] private List<UnitData> playerCardList;
    [SerializeField] private Transform mainSlotParent;
    [SerializeField] private GameObject unitCardPrefab;

    private bool isMenuOpen = false;

    private void Awake()
    {

        GameManager.Instance.RegisterUI(this); 



        if (menuPanel == null)
        {
            GameObject found = GameObject.FindWithTag("Menu");
            if (found != null)
                menuPanel = found;
        }
    }

    private void OnEnable()
    {
        Debug.Log("[SystemUI] OnEnable 호출됨");
        GameManager.Instance.RegisterUI(this);

        SceneManager.sceneLoaded += OnSceneLoaded;

        // 씬이 이미 로드된 경우 수동 실행
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainGame")
        {
            AssignDependencies();
            InitializeUI();
        }
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;

        if (volumeSlider != null)
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);

        if (selectionManager != null)
            selectionManager.UnitDataList.Unsubscribe(OnUnitListChanged);
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"[SystemUI] OnSceneLoaded 호출됨 - 씬: {scene.name}");

        if (scene.name == "MainGame")
        {
            AssignDependencies();
            InitializeUI();
        }
    }

    private void AssignDependencies()
    {
        if (sunRainManager == null)
            sunRainManager = FindObjectOfType<SunRainManager>();

        if (selectionManager == null)
            selectionManager = FindObjectOfType<UnitCardSelectionManager>();

        if (volumeSlider == null)
            volumeSlider = FindObjectOfType<Slider>();

        if (mainSlotParent == null)
            mainSlotParent = GameObject.Find("UnitCardPool")?.transform;

        if (menuPanel == null)
            menuPanel = GameObject.FindWithTag("Menu");

        if (gameOverPanel == null)
        {
            GameObject go = GameObject.FindWithTag("GameOver");
            if (go != null) gameOverPanel = go;
        }

        if (victoryPanel == null)
        {
            GameObject v = GameObject.Find("VictoryUI");
            if (v != null) victoryPanel = v;
        }

        //  초기 비활성화 처리
        if (gameOverPanel) gameOverPanel.SetActive(false);
        if (victoryPanel) victoryPanel.SetActive(false);

        Debug.Log($"[SystemUI] GameOverPanel 할당 상태: {(gameOverPanel == null ? "null" : gameOverPanel.name)}");
    }

    private void InitializeUI()
    {
        if (volumeSlider != null && GameManager.Instance?.Audio != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            float initialVolume = GameManager.Instance.Audio.GetBgmVolume();
            volumeSlider.value = initialVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        CloseMenu();

        var playerUnits = GameManager.Instance?.Player?.SelectedUnits;
        if (selectionManager != null && playerUnits != null)
        {
            selectionManager.Init(playerUnits);
            selectionManager.UnitDataList.Subscribe(OnUnitListChanged);
        }

        Debug.Log($"[SystemUI] 초기 카드 수: {playerUnits?.Count ?? 0}");
    }

    public void OnStartGameButton()
    {
        GameManager.Instance.Wave.StartWaves();

        if (sunRainManager == null)
        {
            Debug.LogWarning("[SystemUI] SunRainManager가 씬에 없습니다.");
            return;
        }

        sunRainManager.StartRain();
        CleanSstMainCardSlots();
    }

    public void StartGame(string sceneName)
    {
        ResumeGame();
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
        GameManager.Instance.Wave?.ResetState(); // 웨이브 초기화
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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
        menuPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void CloseMenu()
    {
        if (menuPanel == null) return;
        isMenuOpen = false;
        menuPanel.SetActive(false);
        Time.timeScale = 1f;
    }

    private void ResumeGame() => Time.timeScale = 1f;

    private void OnVolumeChanged(float value)
    {
        GameManager.Instance.Audio.SetBgmVolume(value);
    }

    private void CleanSstMainCardSlots()
    {
        foreach (Transform slot in mainSlotParent)
        {
            if (slot.childCount == 0) continue;

            GameObject oldCard = slot.GetChild(0).gameObject;
            UnitData data = oldCard.GetComponent<UnitCardSelector>()?.GetData();
            if (data == null) continue;

            Destroy(oldCard);

            GameObject newCard = Instantiate(unitCardPrefab, slot);
            newCard.transform.localPosition = Vector3.zero;
            newCard.transform.localRotation = Quaternion.identity;
            newCard.transform.localScale = Vector3.one;

            UnitCardUI ui = newCard.GetComponent<UnitCardUI>();
            if (ui != null) ui.Setup(data);
        }
    }

    private void OnUnitListChanged(List<UnitData> newList)
    {
        Debug.Log($"[SystemUI] 구독자 알림: 유닛 카드 수 = {newList.Count}");
    }

    public void ShowGameOverUI()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("GameOverPanel이 UIManager에 할당되지 않았습니다.");
        }
    }

    public void ShowVictoryUI()
    {
        if (victoryPanel != null)
        {
            victoryPanel.SetActive(true);
        }
        else
        {
            Debug.LogWarning("[SystemUI] VictoryPanel이 연결되지 않았습니다.");
        }
    }
}
