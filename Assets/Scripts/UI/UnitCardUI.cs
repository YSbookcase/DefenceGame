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

        // 배치 가능한 유닛 프리뷰 생성
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

        // 기존 UI 미리보기 제거 → 대신 3D 프리팹을 사용
        previewObject = Instantiate(unitData.unitPrefab);
        previewObject.name = "PreviewUnit";
        SetLayerRecursively(previewObject.transform, LayerMask.NameToLayer("Ignore Raycast")); // 클릭 방지용
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (previewObject == null)
            return;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, LayerMask.GetMask("Tile")))
        {
            Vector3 pos = hit.collider.transform.position;
            pos.y += 0.5f; // 타일 위로 약간 떠 있도록
            previewObject.transform.position = pos;

            // Z+ 방향을 보던 모델을 X+ 방향으로 회전
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
                        Debug.Log("돈이 부족합니다.");
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
