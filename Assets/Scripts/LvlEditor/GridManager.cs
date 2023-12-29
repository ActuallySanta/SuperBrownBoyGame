using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridManager : MonoBehaviour
{
    [SerializeField] private int gridWidth, gridHeight;

    [SerializeField] private Tiles tilePrefab;

    [SerializeField] private Transform mainCam;

    [SerializeField] private Tilemap gridTileMap;

    [SerializeField] private Tile offsetTile,baseTile;

    private void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {

                var isOffset = (i % 2 == 0 && j % 2 != 0) || (i % 2 != 0 && j % 2 == 0);
                if (isOffset)
                {
                    gridTileMap.SetTile(new Vector3Int(i,j,0),offsetTile);
                }
                else
                {
                    gridTileMap.SetTile(new Vector3Int(i, j, 0), baseTile);
                }
            }
        }

        mainCam.transform.position = new Vector3(((float)gridWidth / 2) - .5f, ((float)gridHeight / 2) - .5f, mainCam.position.z);
    }
}
