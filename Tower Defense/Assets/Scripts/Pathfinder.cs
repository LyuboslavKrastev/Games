using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();

    [SerializeField] private Waypoint _startWaypoint;
    [SerializeField] private Waypoint _finishWaypoint;

    private Vector2Int[] _directions =
    {
        // y is mapping to our z
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };

    void Start()
    {
        LoadBlocks();

        ColorStartAndFinish();

        ExploreNeighbours();
    }

    private void ExploreNeighbours()
    {
        foreach (Vector2Int direction in _directions)
        {
            Vector2Int explorationCoordinates = _startWaypoint.GridPosition + direction;
            if (_grid.ContainsKey(explorationCoordinates))
            {
                _grid[explorationCoordinates].SetTopColor(Color.blue);
                Debug.Log($"Exploring {explorationCoordinates}");
            }
            else
            {
                Debug.Log($"No path available at {explorationCoordinates}");
            }
        }
    }

    private void ColorStartAndFinish()
    {
        _startWaypoint.SetTopColor(Color.yellow);
        _finishWaypoint.SetTopColor(Color.magenta);
    }

    private void LoadBlocks()
    {
        IEnumerable<Waypoint> waypoints = FindObjectsOfType<Waypoint>();

        foreach (var waypoint in waypoints)
        {
            Vector2Int gridPosition = waypoint.GridPosition;

            if (_grid.ContainsKey(gridPosition))
            {
                Debug.LogWarning($"Skipping overlapping cube {waypoint}");
                continue;
            }

            _grid.Add(gridPosition, waypoint);
        }
        
    }
}
