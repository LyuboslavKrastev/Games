using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int _gridSize = 10;

    public Vector2Int GridPosition
    {
        get
        {
            return new Vector2Int(
                Mathf.RoundToInt(transform.position.x / _gridSize),
                Mathf.RoundToInt(transform.position.z / _gridSize)
            );
        }
    }

    public int GridSize 
    {
        get
        {
            return _gridSize;
        }  
    }

    public void SetTopColor(Color color)
    {
        MeshRenderer topMeshRenderer = transform.Find("Top").GetComponent<MeshRenderer>();

        topMeshRenderer.material.color = color;
    }
}
