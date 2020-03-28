using System;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Tower : MonoBehaviour
{
    [SerializeField] Transform _objectToRotate;
    [SerializeField] Transform _targetEnemy;
    [SerializeField] ParticleSystem _projectileParticle;

    private float _attackRange = 50f;

    void Update()
    {
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

    private void Shoot(bool canShoot)
    {
        EmissionModule emissionModule = _projectileParticle.emission;
        emissionModule.enabled = canShoot;
    }
}
