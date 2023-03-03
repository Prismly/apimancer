using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class CustomTile : UnityEngine.Tilemaps.TileBase
{
    public Sprite Sprite;
    public GameObject Prefab;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        tileData.sprite = Application.isPlaying ? null : Sprite;
        tileData.gameObject = Prefab;
    }

    public override bool StartUp(Vector3Int position, ITilemap tilemap, GameObject go)
    {
        if (go == null)
        {
            return false;
        }
        Cell cell = go.GetComponent<Cell>();
        if (cell != null)
        {
            CellManager.Instance?.AddCell(cell, (Vector2Int)position);
        }
        return true;
    }
}
