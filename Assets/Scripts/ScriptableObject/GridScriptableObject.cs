using UnityEngine;

[CreateAssetMenu(fileName = "New Grid", menuName = "GridSize")]
public class GridScriptableObject : ScriptableObject
{
    public int width;
    public int height;
    public float cellSize;
    public Vector3 originPosition;
}
