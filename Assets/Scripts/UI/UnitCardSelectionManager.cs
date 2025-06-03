using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UnitCardSelectionManager : MonoBehaviour
{
    [Header("½½·Ô ºÎ¸ð")]
    [SerializeField] private List<Transform> selectSlots;  // 24°³ÀÇ ºó ½½·Ô
    [SerializeField] private List<Transform> mainSlots;    // 6°³ÀÇ ¸ÞÀÎ ½½·Ô

    [Header("Ä«µå ÇÁ¸®ÆÕ")]
    [SerializeField] private GameObject selectCardPrefab;
    [SerializeField] private GameObject mainCardPrefab;

    private List<UnitCardSelector> selectedCards = new();

    public void Init(List<UnitData> playerCardList)
    {
        for (int i = 0; i < playerCardList.Count && i < selectSlots.Count; i++)
        {
            var slot = selectSlots[i];
            var go = Instantiate(selectCardPrefab, slot);
            var card = go.GetComponent<UnitCardSelector>();
            card.Setup(playerCardList[i], this);
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

        card.transform.SetParent(emptySlot);
        selectedCards.Add(card);
    }

    public void TryDeselectCard(UnitCardSelector card)
    {
        Transform emptySlot = selectSlots.FirstOrDefault(s => s.childCount == 0);
        if (emptySlot == null)
            return;

        card.transform.SetParent(emptySlot);
        selectedCards.Remove(card);
    }

    public List<UnitData> GetSelectedUnits()
    {
        return selectedCards.Select(c => c.GetData()).ToList();
    }

    public bool IsMainSlot(Transform slot)
    {
        return mainSlots.Contains(slot);
    }


}
