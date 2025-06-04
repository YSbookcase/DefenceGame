using UnityEngine;

public static class GridUtility
{
    public static Vector3 SnapToGrid(Vector3 rawPos, float cellSize)
    {
        float x = Mathf.Round(rawPos.x / cellSize) * cellSize;
        float z = Mathf.Round(rawPos.z / cellSize) * cellSize;
        return new Vector3(x, rawPos.y, z);
    }
}
