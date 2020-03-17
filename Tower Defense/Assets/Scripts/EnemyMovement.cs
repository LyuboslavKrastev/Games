using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    void Start()
    {
        Pathfinder pathfinder = FindObjectOfType<Pathfinder>();

        Stack<Waypoint> path = pathfinder.GetPath();

        StartCoroutine(FollowPath(path));
    }

    private IEnumerator FollowPath(Stack<Waypoint> path)
    {
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
