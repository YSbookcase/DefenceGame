using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SystemUI : MonoBehaviour
{
    [SerializeField] private GameObject menuPanel;
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

        // 유닛 카드 선택창에 유닛 카드 배치
        if (selectionManager != null)
        {
            selectionManager.Init(playerCardList); // 내부적으로 unitDataList.Value에 할당됨
            selectionManager.UnitDataList.Subscribe(OnUnitListChanged);
        }
        Debug.Log($"[SystemUI] 초기 카드 수: {playerCardList.Count}");

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

    public void OnStartGameButton()
    {
        WaveManager.Instance.StartWaves();
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

}
