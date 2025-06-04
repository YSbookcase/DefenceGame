using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitCardUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] private RawImage rawImage;
    [SerializeField] private UnitData unitData;
    [SerializeField] private CanvasGroup canvasGroup;

    private RectTransform dragPreview;
    private Transform originalParent;
    private GameObject previewObject;


    private void Update()
    {
        UpdateVisual();
    }
    public void Setup(UnitData data)
    {
        unitData = data;
        if (data.Icon != null)
            rawImage.texture = data.Icon.texture;

        UpdateVisual();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        if (GameManager.Instance.Player.Money.Value < unitData.cost)
            return;

        // ��ġ ������ ���� ������ ����
        GameManager.Instance.Placer.StartPlacing(unitData);
    }



    private void UpdateVisual()
    {
        int money = GameManager.Instance.Player.Money.Value;
        bool canBuy = money >= unitData.cost;
        rawImage.color = canBuy ? Color.white : new Color(0.5f, 0.5f, 0.5f, 1f);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (GameManager.Instance.Player.Money.Value < unitData.cost)
            return;

        canvasGroup.blocksRaycasts = false;

        // ���� UI �̸����� ���� �� ��� 3D �������� ���
        previewObject = Instantiate(unitData.unitPrefab);
        previewObject.name = "PreviewUnit";
        SetLayerRecursively(previewObject.transform, LayerMask.NameToLayer("Ignore Raycast")); // Ŭ�� ������
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (previewObject == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Tile")))
        {
            Vector3 pos = hit.collider.transform.position;
            pos.y += 0.5f; // Ÿ�� ���� �ణ �� �ֵ���
            previewObject.transform.position = pos;

            // Z+ ������ ���� ���� X+ �������� ȸ��
            previewObject.transform.rotation = Quaternion.Euler(0, 90, 0);
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.blocksRaycasts = true;

        if (previewObject != null)
            Destroy(previewObject);

        if (!EventSystem.current.IsPointerOverGameObject())
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100f))
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null && !tile.isOccupied)
                {
                    if (GameManager.Instance.Player.TrySpendMoney(unitData.cost))
                    {
                        tile.isOccupied = true;
                        Vector3 spawnPos = tile.transform.position;
                        GameObject unit = Instantiate(unitData.unitPrefab, spawnPos, Quaternion.Euler(0, 90, 0));
                        unit.name = unitData.unitName;

                        Unit unitScript = unit.GetComponent<Unit>();
                        if (unitScript != null)
                        {
                            unitScript.currentTile = tile;
                            unitScript.Init(unitData);
                        }

                    }
                    else
                    {
                        Debug.Log("���� �����մϴ�.");
                    }
                }
            }
        }
    }

    private void SetLayerRecursively(Transform t, int layer)
    {
        t.gameObject.layer = layer;
        foreach (Transform child in t)
            SetLayerRecursively(child, layer);
    }

}
