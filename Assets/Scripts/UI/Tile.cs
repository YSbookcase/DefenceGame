using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool isOccupied = false;

    private void Start()
    {
        GameManager.Instance.Tile.RegisterTile(this);
    }
}