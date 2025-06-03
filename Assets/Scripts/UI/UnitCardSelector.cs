using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UnitCardSelector : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private Image iconImage;
    private UnitData unitData;
    private UnitCardSelectionManager manager;

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
        // �θ� ���� �������� Ȯ��
        if (manager.IsMainSlot(transform.parent))
            manager.TryDeselectCard(this);
        else
            manager.TrySelectCard(this);
    }
}
