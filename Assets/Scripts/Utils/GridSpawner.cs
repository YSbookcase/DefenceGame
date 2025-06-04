using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public int width = 9;
    public int height = 5;
    public float cellSize = 3f;
    public GameObject tilePrefab;
    public int border = 1;
    public GameObject outerTilePrefab;

    // 격자 색상
    private readonly Color lightColor = new Color(0.9f, 0.9f, 0.9f);
    private readonly Color darkColor = new Color(0.7f, 0.7f, 0.7f);

    // 타일 Layer 번호 (8번: "Tile")
    private const int tileLayer = 8;

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
        // 기존 자식 오브젝트 제거
        for (int i = transform.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject);
        }

        int totalWidth = width + border * 2;
        int totalHeight = height + border * 2;

        for (int x = 0; x < totalWidth; x++)
        {
            for (int z = 0; z < totalHeight; z++)
            {
                Vector3 pos = new Vector3(x * cellSize, 0f, z * cellSize);
                bool isInner = x >= border && x < width + border &&
                               z >= border && z < height + border;

                GameObject prefabToUse = isInner ? tilePrefab : outerTilePrefab;

                GameObject tile = Instantiate(prefabToUse, transform);
                tile.transform.localPosition = pos;
                tile.transform.localRotation = prefabToUse.transform.localRotation;
                tile.transform.localScale = prefabToUse.transform.localScale;
                tile.name = $"{(isInner ? "Tile" : "Outer")}_{x}_{z}";

                // 내부 타일일 경우에만 Layer 설정 및 색상 적용
                if (isInner)
                {
                    tile.layer = tileLayer;

                    Renderer renderer = tile.GetComponent<Renderer>();
                    if (renderer != null)
                    {
                        renderer.material.color = (x + z) % 2 == 0 ? lightColor : darkColor;
                    }
                }
            }
        }
    }
}
