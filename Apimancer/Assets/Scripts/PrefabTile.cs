using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

[CreateAssetMenu]
public class PrefabTile : UnityEngine.Tilemaps.TileBase
{
    public Sprite Sprite;
    public GameObject Prefab;

    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {
        if (!Application.isPlaying) tileData.sprite = Sprite;
        else tileData.sprite = null;
 
        if (Prefab != null) tileData.gameObject = Prefab;
    }
}
