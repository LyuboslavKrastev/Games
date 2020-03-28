using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float _cooldown = 6f;
    [SerializeField] private EnemyMovement _enemyPrefab; 
    void Start()
    {
        StartCoroutine(SpawnEnemies());
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            GameObject
                .Instantiate(
                _enemyPrefab.gameObject, transform.position, Quaternion.identity);

            yield return new WaitForSeconds(_cooldown);
        }    
    }
}
