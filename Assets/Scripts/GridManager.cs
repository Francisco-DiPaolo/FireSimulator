using UnityEngine;

public class GridManager : MonoBehaviour
{
    public int rows;
    public int columns;

    public float tileSize;
    
    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        GameObject referenceTile = Resources.Load<GameObject>("Prefabs/TileGrass");

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                GameObject tile = Instantiate(referenceTile, transform);

                float posX = column * tileSize;
                float posY = row * -tileSize;

                tile.transform.position = new Vector2(posX, posY);
            }
        }
    }
}
