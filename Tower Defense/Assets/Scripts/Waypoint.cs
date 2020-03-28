using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int _gridSize = 10;

    public bool canBuildOn = true;

    public bool IsExplored { get; set; } = false;

    [SerializeField] private Tower _towerPrefab;

    public Waypoint exploredFrom; 

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

    void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (canBuildOn)
            {
                GameObject.Instantiate(_towerPrefab, transform.position, Quaternion.identity);
            }
            else
            {
                print("can not build here");
            }         
        }
    }
}
