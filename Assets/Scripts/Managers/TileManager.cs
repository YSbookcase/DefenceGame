using System.Collections.Generic;
using UnityEngine;
using DesignPattern;

public class TileManager : Singleton<TileManager>
{
    [SerializeField] private float cellSize = 3f;

    private Dictionary<Vector2Int, Tile> tileMap = new();

    /// <summary>
    /// 타일 등록 (Tile.cs에서 호출)
    /// </summary>
    public void RegisterTile(Tile tile)
    {
        if (tile == null)
        {
            Debug.LogError("등록하려는 타일이 null입니다.");
            return;
        }

        Vector2Int gridPos = WorldToGrid(tile.transform.position);
        if (!tileMap.ContainsKey(gridPos))
        {
            tileMap.Add(gridPos, tile);
        }
        else
        {
            Debug.LogWarning($"이미 등록된 타일: {gridPos}");
        }
    }

    /// <summary>
    /// 월드 좌표로부터 해당 타일을 가져옴
    /// </summary>
    public Tile GetTileAt(Vector3 worldPosition)
    {
        Vector2Int gridPos = WorldToGrid(worldPosition);
        tileMap.TryGetValue(gridPos, out Tile tile);
        return tile;
    }

    /// <summary>
    /// 그리드 좌표 반환 (x, z 기준)
    /// </summary>
    private Vector2Int WorldToGrid(Vector3 pos)
    {
        int x = Mathf.RoundToInt(pos.x / cellSize);
        int z = Mathf.RoundToInt(pos.z / cellSize);
        return new Vector2Int(x, z);
    }

    /// <summary>
    /// 모든 타일 반환 (선택 영역 표시 등에 사용 가능)
    /// </summary>
    public IEnumerable<Tile> GetAllTiles() => tileMap.Values;
}
