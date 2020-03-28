﻿using System.Collections.Generic;
using UnityEngine;

public class Pathfinder : MonoBehaviour
{
    private Dictionary<Vector2Int, Waypoint> _grid = new Dictionary<Vector2Int, Waypoint>();

    private Queue<Waypoint> _waypointQueue = new Queue<Waypoint>();

    private Waypoint currentSearchCenter; // The current search center

    bool isAtFinish = false;

    [SerializeField] private Waypoint _startWaypoint;
    [SerializeField] private Waypoint _finishWaypoint;

    private Stack<Waypoint> _path = new Stack<Waypoint>();

    private Vector2Int[] _directions =
    {
        // y is mapping to our z
        Vector2Int.up,
        Vector2Int.right,
        Vector2Int.down,
        Vector2Int.left
    };


    public Stack<Waypoint> GetPath()
    {
        LoadBlocks();
        ColorStartAndFinish();
        BreadthFirstSearch();
        CreatePath();

        return _path;
    }

    private void CreatePath()
    {
        _path.Push(_finishWaypoint);

        Waypoint previous = _finishWaypoint.exploredFrom;

        while (previous != _startWaypoint)
        {
            // add intermidiate waypoints
            _path.Push(previous);
            previous = previous.exploredFrom;
        }

        // add start waypoint
        _path.Push(_startWaypoint);
    }

    private void BreadthFirstSearch()
    {
        _waypointQueue.Enqueue(_startWaypoint);

        while (_waypointQueue.Count > 0 && isAtFinish == false)
        {
          
            currentSearchCenter = _waypointQueue.Dequeue();
            currentSearchCenter.IsExplored = true;

            StopIfAtFinish();

            ExploreNeighbours();           
        }
    }

    private void StopIfAtFinish()
    {
        if (currentSearchCenter == _finishWaypoint)
        {
            isAtFinish = true;
        }
    }

    private void ExploreNeighbours()
    {
        if (isAtFinish)
        {
            return;
        }
        foreach (Vector2Int direction in _directions)
        {
            Vector2Int neighbourCoordinates = currentSearchCenter.GridPosition + direction;
            if (_grid.ContainsKey(neighbourCoordinates))
            {
                QueueNewNeighbours(neighbourCoordinates);
            }
        }
    }

    private void QueueNewNeighbours(Vector2Int neighbourCoordinates)
    {
        Waypoint neighbour = _grid[neighbourCoordinates];
        if (neighbour.IsExplored || _waypointQueue.Contains(neighbour))
        {
            return;
        }
        neighbour.exploredFrom = currentSearchCenter;
        _waypointQueue.Enqueue(neighbour);
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
