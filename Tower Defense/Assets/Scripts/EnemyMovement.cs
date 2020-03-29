using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    private float _movementCooldown = 0.75f;
    [SerializeField] ParticleSystem _baseAttackParticles;
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
            yield return new WaitForSeconds(_movementCooldown);
        }
        Explode();
    }

    private void Explode()
    {
        ParticleSystem baseAttackFx = GameObject.Instantiate(_baseAttackParticles, transform.position, Quaternion.identity);
        baseAttackFx.Play();
        float destroyDelay = baseAttackFx.main.duration;
        Destroy(baseAttackFx.gameObject, destroyDelay);
        Destroy(this.gameObject);
    }
}
