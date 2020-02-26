using UnityEngine;

[ExecuteInEditMode]
[SelectionBase]
public class CubeEditor : MonoBehaviour
{   
    private Waypoint _waypoint;

    void Awake()
    {
        _waypoint = GetComponent<Waypoint>();

        if (_waypoint == null)
        {
            Debug.LogError("Waypoint is NULL!");
        }
    }

    void Update()
    {
        int gridSize = _waypoint.GridSize;
        Vector2Int gridPosition = _waypoint.GridPosition;

        SnapToGrid(gridSize, gridPosition);
        UpdateLabel(gridPosition);
    }

    private void SnapToGrid(int gridSize, Vector2Int gridPosition)
    {
        transform.position = new Vector3 (
                gridPosition.x * gridSize,
                0f,
                gridPosition.y * gridSize
            );
    }

    private void UpdateLabel(Vector2Int gridPosition)
    {
        TextMesh textMesh = GetComponentInChildren<TextMesh>();

        if (textMesh == null)
        {
            Debug.LogError("TextMesh is NULL!");
        }

        string labelText = $"{gridPosition.x}, {gridPosition.y}";
        textMesh.text = labelText;
        gameObject.name = "Custom Cube: " + labelText;
    }
}
