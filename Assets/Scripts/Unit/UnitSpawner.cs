using System.Collections.Generic;
using UnityEngine;

public class UnitCardSpawner : MonoBehaviour
{
    [SerializeField] private List<Transform> cardSlots; // ½½·Ô À§Ä¡ (ºó ½½·Ô ¿ÀºêÁ§Æ®)
    [SerializeField] private GameObject cardPrefab;     // UnitCardUI ÇÁ¸®ÆÕ
    [SerializeField] private List<UnitData> unitDataList;

    private void Start()
    {
        for (int i = 0; i < cardSlots.Count && i < unitDataList.Count; i++)
        {
            GameObject card = Instantiate(cardPrefab, cardSlots[i]);
            card.transform.localPosition = Vector3.zero;
            card.transform.localRotation = Quaternion.identity;
            card.transform.localScale = Vector3.one;

            UnitCardUI ui = card.GetComponent<UnitCardUI>();
            ui.Setup(unitDataList[i]);
        }
    }
}
