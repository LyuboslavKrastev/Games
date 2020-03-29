using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    private float _spawnCooldown = 1f;
    [SerializeField] private EnemyMovement _enemyPrefab;
    [SerializeField] private Transform enemyParentTransform;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject enemy = GameObject
                .Instantiate(
                _enemyPrefab.gameObject, transform.position, Quaternion.identity);

            enemy.transform.parent = enemyParentTransform;

            yield return new WaitForSeconds(_spawnCooldown);
        }    
    }
}
