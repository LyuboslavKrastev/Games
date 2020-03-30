using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemySpawner : MonoBehaviour
{
    private float _spawnCooldown = 1f;
    [SerializeField] private EnemyMovement _enemyPrefab;
    [SerializeField] private Transform enemyParentTransform;
    [SerializeField] private Text _spawnedEnemies;
    [SerializeField] AudioClip _enemySpawnSFX;

    private int _score;
    void Start()
    {
        StartCoroutine(SpawnEnemies());
        UpdateSpawnedEnemiesText();
    }

    private void UpdateSpawnedEnemiesText()
    {
        _spawnedEnemies.text = $"Score: {_score}";
    }

    IEnumerator SpawnEnemies()
    {
        while (true)
        {
            _score++;
            GetComponent<AudioSource>().PlayOneShot(_enemySpawnSFX);
            UpdateSpawnedEnemiesText();

            GameObject enemy = GameObject.Instantiate(
                _enemyPrefab.gameObject, transform.position, Quaternion.identity);

            enemy.transform.parent = enemyParentTransform;

            yield return new WaitForSeconds(_spawnCooldown);
        }    
    }
}
