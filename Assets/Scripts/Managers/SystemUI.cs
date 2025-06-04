using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using DesignPattern;


public class SystemUI : MonoBehaviour
{ 
    [SerializeField] private GameObject menuPanel;
    [SerializeField] private GameObject gameOverPanel;

    [SerializeField] private Slider volumeSlider;

    [SerializeField] private SunRainManager sunRainManager;

    
    [SerializeField] private UnitCardSelectionManager selectionManager;
    [SerializeField] private List<UnitData> playerCardList;

    [SerializeField] private Transform mainSlotParent;
    [SerializeField] private GameObject unitCardPrefab;
  


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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && menuPanel != null)
        {
            ToggleMenu();
        }
    }

    private void OnEnable()
    {
        Debug.Log("[SystemUI] OnEnable 호출됨");
        SceneManager.sceneLoaded += OnSceneLoaded;

        // 씬이 이미 로드된 상태일 수도 있으므로 즉시 검사
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "MainGame")
        {
            Debug.Log("[SystemUI] 현재 씬이 MainGame → 직접 초기화 실행");
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
        AssignDependencies();
        InitializeUI();
    }

    private void AssignDependencies()
    {
        if (sunRainManager == null)
            sunRainManager = FindObjectOfType<SunRainManager>();
        Debug.Log("[SystemUI] SunRainManager 지나침");
        if (selectionManager == null)
            selectionManager = FindObjectOfType<UnitCardSelectionManager>();

        if (volumeSlider == null)
            volumeSlider = FindObjectOfType<Slider>();

        if (mainSlotParent == null)
            mainSlotParent = GameObject.Find("UnitCardPool")?.transform;

        if (menuPanel == null)
            menuPanel = GameObject.FindWithTag("Menu");

        if (gameOverPanel == null)
            gameOverPanel = GameObject.Find("GameOverUI");
    }

    private void InitializeUI()
    {
        // 볼륨 슬라이더
        if (volumeSlider != null && GameManager.Instance?.Audio != null)
        {
            volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
            float initialVolume = GameManager.Instance.Audio.GetBgmVolume();
            volumeSlider.value = initialVolume;
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        // 메뉴 닫기
        if (menuPanel != null)
            CloseMenu();

        // 유닛 카드 초기화
        var playerUnits = GameManager.Instance?.Player?.SelectedUnits;
        if (selectionManager != null && playerUnits != null)
        {
            selectionManager.Init(playerUnits);
            selectionManager.UnitDataList.Subscribe(OnUnitListChanged);
        }

        Debug.Log($"[SystemUI] 초기 카드 수: {playerUnits?.Count ?? 0}");
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

}
