using UnityEngine;

public class GridSpawner : MonoBehaviour
{
    public int width = 5;
    public int height = 5;
    public float cellSize = 3f;
    public GameObject tilePrefab;
    public int border = 1;
    public GameObject outerTilePrefab; // 외곽용 프리팹

    [ContextMenu("Generate Grid")]
    public void GenerateGrid()
    {
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
                GameObject prefabToUse;

                // 내부 영역이면 기본 타일, 외곽이면 다른 프리팹 사용
                if (x >= border && x < width + border &&
                    z >= border && z < height + border)
                {
                    prefabToUse = tilePrefab;
                }
                else
                {
                    prefabToUse = outerTilePrefab;
                }

                GameObject tile = Instantiate(prefabToUse, transform);
                tile.transform.localPosition = pos;
                tile.transform.localRotation = prefabToUse.transform.localRotation;
                tile.transform.localScale = prefabToUse.transform.localScale;
                tile.name = $"{(prefabToUse == tilePrefab ? "Tile" : "Outer")}_{x}_{z}";
            }
        }
    }
}