using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DesignPattern;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnitCardSelectionManager : MonoBehaviour
{
    [Header("슬롯 부모")]
    [SerializeField] private List<Transform> selectSlots;  // 24개의 빈 슬롯
    [SerializeField] private List<Transform> mainSlots;    // 6개의 메인 슬롯

    [Header("카드 프리팹")]
    [SerializeField] private GameObject selectCardPrefab;
    [SerializeField] private GameObject mainCardPrefab;

    // 카드 목록을 ObservableProperty로 래핑
    [SerializeField] private ObservableProperty<List<UnitData>> unitDataList = new(new List<UnitData>());
    public ObservableProperty<List<UnitData>> UnitDataList => unitDataList;

    private List<UnitCardSelector> selectedCards = new();



    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.name == "MainGame")
        {
            Init(GameManager.Instance.Player.SelectedUnits);
        }
    }





    public void Init(List<UnitData> playerCardList)
    {
        Debug.Log($"[UnitCardSelectionManager] Init 호출됨, 카드 수: {playerCardList.Count}");

        AutoAssignSlots(); // 슬롯 자동 할당
        ClearAllSlots();

        // ObservableProperty에 값 반영
        unitDataList.Value = playerCardList;

        for (int i = 0; i < playerCardList.Count && i < selectSlots.Count; i++)
        {
            GameObject card = Instantiate(selectCardPrefab, selectSlots[i]);
            card.transform.localPosition = Vector3.zero;
            card.transform.localRotation = Quaternion.identity;
            card.transform.localScale = Vector3.one;

            var selector = card.GetComponent<UnitCardSelector>();

            if (selector == null)
            {
                Debug.LogError($"→ UnitCardSelector가 프리팹에 없음!");
                continue;
            }

            selector.Setup(playerCardList[i], this);
        }
    }

    public void TrySelectCard(UnitCardSelector card)
    {
        if (selectedCards.Contains(card))
            return;

        if (selectedCards.Count >= mainSlots.Count)
            return;

        Transform emptySlot = mainSlots.FirstOrDefault(s => s.childCount == 0);
        if (emptySlot == null)
            return;

        card.transform.SetParent(emptySlot, false); // ← 반드시 worldPositionStays: false
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.identity;
        card.transform.localScale = Vector3.one;

        selectedCards.Add(card);
        card.SetSelected(true);
    }

   

    public void TryDeselectCard(UnitCardSelector card)
    {
        Transform emptySlot = selectSlots.FirstOrDefault(s => s.childCount == 0);
        if (emptySlot == null)
            return;

        card.transform.SetParent(emptySlot, false); // UI 좌표계 기준으로 붙이기
        card.transform.localPosition = Vector3.zero;
        card.transform.localRotation = Quaternion.identity;
        card.transform.localScale = Vector3.one;

        selectedCards.Remove(card);
        card.SetSelected(false);
    }

    public List<UnitData> GetSelectedUnits()
    {
        return selectedCards.Select(c => c.GetData()).ToList();
    }

    public bool IsMainSlot(Transform slot)
    {
        return mainSlots.Contains(slot);
    }

    private void AutoAssignSlots()
    {
        // Select 슬롯 (USC0 ~ USC23)
        selectSlots = new List<Transform>();
        for (int i = 0; i < 24; i++)
        {
            string slotName = $"USC{i}";
            GameObject slotObj = GameObject.Find(slotName);
            if (slotObj != null)
                selectSlots.Add(slotObj.transform);
            else
                Debug.LogWarning($"[UnitCardSelectionManager] Select 슬롯 {slotName} 를 찾을 수 없습니다.");
        }

        // Main 슬롯 (UMC0 ~ UMC5)
        mainSlots = new List<Transform>();
        for (int i = 0; i < 6; i++)
        {
            string slotName = $"UMC{i}";
            GameObject slotObj = GameObject.Find(slotName);
            if (slotObj != null)
                mainSlots.Add(slotObj.transform);
            else
                Debug.LogWarning($"[UnitCardSelectionManager] Main 슬롯 {slotName} 를 찾을 수 없습니다.");
        }
    }

    private void ClearAllSlots()
    {
        foreach (Transform slot in selectSlots.Concat(mainSlots))
        {
            foreach (Transform child in slot)
            {
                Destroy(child.gameObject);
            }
        }

        selectedCards.Clear();
    }

}
