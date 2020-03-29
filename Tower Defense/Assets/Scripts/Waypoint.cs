using UnityEngine;

public class Waypoint : MonoBehaviour
{
    private const int _gridSize = 10;

    public bool CanBuildOn { get; set; } = true;

    public bool IsExplored { get; set; } = false;

    private TowerFactory _towerFactory;

    void Start()
    {
        _towerFactory = FindObjectOfType<TowerFactory>();
    }

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
            if (CanBuildOn)
            {
                _towerFactory.AddTower(this);
            }
            else
            {
                print("cannot build here");
            }         
        }
    }
}
