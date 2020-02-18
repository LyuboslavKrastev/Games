using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;
    void Start()
    {
        PrintWayPoints();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void PrintWayPoints()
    {
        foreach (var waypoint in path)
        {
            Debug.Log(waypoint.name);
        }
    }
}
