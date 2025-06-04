using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DesignPattern;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UnitCardSelectionManager : MonoBehaviour
{
    [Header("���� �θ�")]
    [SerializeField] private List<Transform> selectSlots;  // 24���� �� ����
    [SerializeField] private List<Transform> mainSlots;    // 6���� ���� ����

    [Header("ī�� ������")]
    [SerializeField] private GameObject selectCardPrefab;
    [SerializeField] private GameObject mainCardPrefab;

    // ī�� ����� ObservableProperty�� ����
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
        Debug.Log($"[UnitCardSelectionManager] Init ȣ���, ī�� ��: {playerCardList.Count}");

        AutoAssignSlots(); // ���� �ڵ� �Ҵ�
        ClearAllSlots();

        // ObservableProperty�� �� �ݿ�
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
                Debug.LogError($"�� UnitCardSelector�� �����տ� ����!");
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

        card.transform.SetParent(emptySlot, false); // �� �ݵ�� worldPositionStays: false
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

        card.transform.SetParent(emptySlot, false); // UI ��ǥ�� �������� ���̱�
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
        // Select ���� (USC0 ~ USC23)
        selectSlots = new List<Transform>();
        for (int i = 0; i < 24; i++)
        {
            string slotName = $"USC{i}";
            GameObject slotObj = GameObject.Find(slotName);
            if (slotObj != null)
                selectSlots.Add(slotObj.transform);
            else
                Debug.LogWarning($"[UnitCardSelectionManager] Select ���� {slotName} �� ã�� �� �����ϴ�.");
        }

        // Main ���� (UMC0 ~ UMC5)
        mainSlots = new List<Transform>();
        for (int i = 0; i < 6; i++)
        {
            string slotName = $"UMC{i}";
            GameObject slotObj = GameObject.Find(slotName);
            if (slotObj != null)
                mainSlots.Add(slotObj.transform);
            else
                Debug.LogWarning($"[UnitCardSelectionManager] Main ���� {slotName} �� ã�� �� �����ϴ�.");
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
