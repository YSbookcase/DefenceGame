using UnityEngine;
using DesignPattern;
public class UnitPlacer : MonoBehaviour
{
    private GameObject previewObject;
    private UnitData currentData;

    [SerializeField] private LayerMask tileLayerMask;
    private Camera mainCam;

    private void Start()
    {
        mainCam = Camera.main;
    }

    public void StartPlacing(UnitData data)
    {
        if (previewObject != null)
            Destroy(previewObject);

        currentData = data;
        previewObject = Instantiate(data.unitPrefab);
        SetPreviewVisual(previewObject, true); // ����/���������� ����
    }

    private void Update()
    {
        if (previewObject == null)
            return;

        Ray ray = mainCam.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit, 100f, tileLayerMask))
        {
            previewObject.transform.position = hit.collider.transform.position;

            if (Input.GetMouseButtonDown(0))
            {
                Tile tile = hit.collider.GetComponent<Tile>();
                if (tile != null && !tile.isOccupied && GameManager.Instance.Player.TrySpendMoney(currentData.cost))
                {
                    tile.isOccupied = true;
                    SetPreviewVisual(previewObject, false); // �ð� ȿ�� ����
                    previewObject = null;
                }
            }
            else if (Input.GetMouseButtonDown(1))
            {
                // ���
                Destroy(previewObject);
                previewObject = null;
            }
        }
    }

    private void SetPreviewVisual(GameObject go, bool isPreview)
    {
        var renderers = go.GetComponentsInChildren<Renderer>();
        foreach (var r in renderers)
        {
            foreach (var mat in r.materials)
                mat.color = isPreview ? new Color(1, 1, 1, 0.5f) : Color.white;
        }
    }
}
