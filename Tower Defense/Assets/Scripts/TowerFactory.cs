using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerFactory : MonoBehaviour
{
    [SerializeField] private Tower _towerPrefab;
    [SerializeField] private int _towerLimit = 5;
    [SerializeField] private Transform _towerParentTransform;

    private Queue<Tower> _towerQueue = new Queue<Tower>();
    public void AddTower(Waypoint waypoint)
    {
        int towersPlaced = _towerQueue.Count;
        if (towersPlaced < _towerLimit)
        {
            InstantiateTower(waypoint);
        }
        else
        {
            MoveExistingTower(waypoint);
        }
    }

    private void InstantiateTower(Waypoint waypoint)
    {
        waypoint.CanBuildOn = false;

        Tower tower = GameObject.Instantiate(_towerPrefab, waypoint.transform.position, Quaternion.identity);
        tower.transform.parent = _towerParentTransform; // organize the hierarchy
        tower.Waypoint = waypoint;
        _towerQueue.Enqueue(tower);
    }

    private void MoveExistingTower(Waypoint waypoint)
    {
        waypoint.CanBuildOn = false;

        Tower tower = _towerQueue.Dequeue();
        tower.Waypoint.CanBuildOn = true; // free up the old block
        tower.Waypoint = waypoint;

        tower.transform.position = waypoint.transform.position; // move to the new block

        _towerQueue.Enqueue(tower);


    }
}
