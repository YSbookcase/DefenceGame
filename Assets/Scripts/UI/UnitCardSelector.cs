using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitCardSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    private UnitData unitData;
    private UnitCardSelectionManager manager;

    private bool isSelected = false;
    public void Setup(UnitData data, UnitCardSelectionManager mgr)
    {
        unitData = data;
        manager = mgr;

        if (data.Icon != null)
            iconImage.sprite = data.Icon;
    }

    public UnitData GetData() => unitData;

    public void OnPointerClick(PointerEventData eventData)
    {
        // 부모가 메인 슬롯인지 확인
        if (manager.IsMainSlot(transform.parent))
            manager.TryDeselectCard(this);
        else
            manager.TrySelectCard(this);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;

        // 예: 어두운 색상으로 표시
        iconImage.color = selected ? new Color(0.5f, 0.5f, 0.5f, 1f) : Color.white;
    }



}
