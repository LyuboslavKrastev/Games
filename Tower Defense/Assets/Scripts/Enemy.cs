using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] List<Waypoint> path;
    void Start()
    {
        StartCoroutine(MoveThroughWaypoints());
    }
    private IEnumerator MoveThroughWaypoints()
    {
        foreach (var waypoint in path)
        {
            transform.position = waypoint.transform.position;
            yield return new WaitForSeconds(1f);
        }
    }
}
