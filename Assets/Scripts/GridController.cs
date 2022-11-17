using UnityEngine;
using System.Collections.Generic;

public class GridController : MonoBehaviour
{
    public Grid<int> grid;
    public Grid<GameObject> gameObjectsGrid;
    public GridScriptableObject gridScriptableObject;

    public GameObject prefabWater;
    public GameObject prefabHealthyTree;
    public GameObject prefabDryTree;
    public GameObject prefabMountains;

    public float magnification = 7.0f;
    public int moveNoise;

    private int width, height;
    private int offsetX = 0;
    private int offsetY = 0;

    private float cellSize;

    private Vector3 originPosition;

    private bool gridStarted = false;

    Dictionary<int, GameObject> tileSet;

    List<List<int>> noiseGrid = new List<List<int>>();
    List<List<GameObject>> tileGrid = new List<List<GameObject>>();

    void Awake()
    {
        SetGrid();

        grid = new Grid<int>(width, height, cellSize, originPosition);

        gameObjectsGrid = new Grid<GameObject>(width, height, cellSize, originPosition);
    }

    private void Start()
    {
        CreateTileset();
        if (!gridStarted) InitializeGrids();
    }

    private void SetGrid()
    {
        width = gridScriptableObject.width;
        height = gridScriptableObject.height;
        cellSize = gridScriptableObject.cellSize;
        originPosition = gridScriptableObject.originPosition;
    }

    void InitializeGrids()
    {
        for (int x = 0; x < this.width; x++)
        {
            noiseGrid.Add(new List<int>());
            tileGrid.Add(new List<GameObject>());

            for (int y = 0; y < this.height; y++)
            {
                int tileId = GetIdUsingPerlin(x + moveNoise, y + moveNoise);
                noiseGrid[x].Add(tileId);
                CreateTile(tileId, x, y);
            }
        }

        gridStarted = true;
    }

    void CreateTileset()
    {
        tileSet = new Dictionary<int, GameObject>();
        tileSet.Add(0, prefabWater);
        tileSet.Add(1, prefabHealthyTree);
        tileSet.Add(2, prefabDryTree);
        tileSet.Add(3, prefabMountains);
    }

    int GetIdUsingPerlin(int x, int y)
    {

        float rawPerlin = Mathf.PerlinNoise(
            (x - offsetX) / magnification,
            (y - offsetY) / magnification
        );
        float clampPerlin = Mathf.Clamp01(rawPerlin);
        float scaledPerlin = clampPerlin * tileSet.Count;

        if (scaledPerlin == tileSet.Count)
        {
            scaledPerlin = (tileSet.Count - 1);
        }
        return Mathf.FloorToInt(scaledPerlin);
    }

    void CreateTile(int tileId, int x, int y)
    {
        GameObject tilePrefab = tileSet[tileId];
        GameObject tile = Instantiate(tilePrefab, grid.GetWorldPosition(x, y), Quaternion.identity);

        tileGrid[x].Add(tile);
    }
}
