using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Tower : MonoBehaviour
{
    [SerializeField] private Transform _objectToRotate;
    [SerializeField] private ParticleSystem _projectileParticle;
    [SerializeField] private float _attackRange = 50f;

    private Transform _targetEnemy;

    void Update()
    {
        GetTarget();
        if (_targetEnemy == null)
        {
            Shoot(false);
            return;
        }

        bool isInAttackRange = Vector3.Distance(transform.position, _targetEnemy.position) <= _attackRange;
        if (isInAttackRange)
        {
            _objectToRotate.LookAt(_targetEnemy.position);
            Shoot(true);
        }
        else
        {
            Shoot(false);
        }
    }

    private void GetTarget()
    {
        Health[] sceneEnemies = FindObjectsOfType<Health>();

        if (sceneEnemies.Length == 0)
        {
            return;
        }

        Transform closestEnemyTransform = sceneEnemies[0].transform;

        foreach (var enemy in sceneEnemies)
        {
            closestEnemyTransform = GetClosest(closestEnemyTransform, enemy.transform);
        }

        _targetEnemy = closestEnemyTransform;
    }

    private Transform GetClosest(Transform firstTransform, Transform secondTransform)
    {
        float distanceToFirst = GetDistance(firstTransform);
        float distanceToSecond = GetDistance(secondTransform);
        if (distanceToFirst <= distanceToSecond)
        {
            return firstTransform;
        }
        return secondTransform;
    }

    private float GetDistance(Transform targetTransform)
    {
        float distance = Vector3.Distance(transform.position, targetTransform.position);
        return distance;
    }

    private void Shoot(bool canShoot)
    {
        EmissionModule emissionModule = _projectileParticle.emission;
        emissionModule.enabled = canShoot;
    }
}
