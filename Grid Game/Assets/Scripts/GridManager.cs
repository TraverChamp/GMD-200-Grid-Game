using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GridManager : MonoBehaviour
{
    public event Action<GridTile> TileSelected;

    public int numRows = 50;

    public int numColumns = 60;

    public float padding = 0.1f;

    [SerializeField] private GridTile tilePrefab;
    [SerializeField] private TextMeshProUGUI text;
    private GridTile[] _tiles;

    private void Awake()
    {
        InitGrid();
    }

    public void InitGrid()
    {
        _tiles = new GridTile[numRows*numColumns];
        for (int y = 0; y < numRows; y++)
        {
            for (int x = 0; x < numColumns; x++)
            {
                GridTile tile = Instantiate(tilePrefab, transform);
                Vector2 tilePos = new Vector2(x + (padding*x), y + (padding*y));
                tile.transform.localPosition = tilePos;
                tile.name = $"Tile_{x}_{y}";
                tile.gridManager = this;
                tile.gridCoords = new Vector2Int(x, y);
                _tiles[y * numColumns + x] = tile;
            }
        }
    }

    public void OnTileHoverEnter(GridTile gridTile)
    {
        text.text = gridTile.gridCoords.ToString();
    }

    public void OnTileHoverExit(GridTile gridTile)
    {
        text.text = "___";
    }

    public void OnTileSelected(GridTile gridTile)
    {
        TileSelected?.Invoke(gridTile);
    }

    public GridTile GetTile(Vector2Int pos)
    {
        if (pos.x < 0 || pos.x > numColumns || pos.y < 0 || pos.y > numRows)
        {
            Debug.LogError("Invalid coordinate{pos}");
            return null;
        }
        return _tiles[pos.y * numColumns + pos.x];
    }

    public Vector2Int GetGridPosition(Vector3 worldPosition)
    {
        Vector3 localPosition = transform.InverseTransformPoint(worldPosition);
        int x = Mathf.FloorToInt(localPosition.x);
        int y = Mathf.FloorToInt(localPosition.y);
        return new Vector2Int(x, y);
    }

    public bool IsValidGridPosition(Vector2Int gridPosition)
    {
        return gridPosition.x >= 0 && gridPosition.x < numColumns &&
               gridPosition.y >= 0 && gridPosition.y < numRows;
    }

    public Vector3 GetWorldPosition(Vector2Int gridPosition)
    {
        return transform.TransformPoint(new Vector3(gridPosition.x*1.1f, gridPosition.y*1.1f)); // 0.1 is the padding, position calculation is 1+padding
    }

    public void ResetPlayerWalls()
    {
        GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall");
        foreach (GameObject wall in walls)
        {
            Destroy(wall);
        }
    }

}
