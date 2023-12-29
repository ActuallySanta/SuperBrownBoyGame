using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class LevelEditor : MonoBehaviour
{
    [SerializeField] Tilemap defaultTilemap;
    [SerializeField] GameObject settingTilesText;

    Tilemap currTileMap
    {
        get
        {
            if (LevelManager.Instance.layers.TryGetValue((int)LevelManager.Instance.tiles[selectedTileIndex].tileMap, out Tilemap tilemap))
            {
                return tilemap;
            }
            else
            {
                return defaultTilemap;
            }
        }
    }
    TileBase currTile
    {
        get
        {
            return LevelManager.Instance.tiles[selectedTileIndex].tile;
        }

    }
    [SerializeField] Camera mainCam;

    int selectedTileIndex;
    public bool isSettingTiles = false;

    public enum CurrentPlacingMode
    {
        tiles,
        prefabs,
    }

    private void Update()
    {
        Vector3Int _pos = currTileMap.WorldToCell(mainCam.ScreenToWorldPoint(Input.mousePosition));



        settingTilesText.SetActive(isSettingTiles);

        if (!isSettingTiles) return;

        if (Input.GetMouseButton(0))
        {
            PlaceTile(_pos);
        }

        if (Input.GetMouseButton(1))
        {
            DeleteTile(_pos);
        }
    }

    void PlaceTile(Vector3Int pos)
    {
        currTileMap.SetTile(pos, currTile);
    }

    void DeleteTile(Vector3Int pos)
    {
        currTileMap.SetTile(pos, null);
    }

    public void SetCurrTile(int _tileIndex)
    {
        selectedTileIndex = _tileIndex;
    }
}
