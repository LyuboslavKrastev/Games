using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField]
    private GameObject _enemyContainer;

    private bool _continueSpawning = true;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(this.SpawnRoutine());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // spawn game objects every 5 seconds 
    // Create a coroutine of time IEnumerator -- Yield events 
    // while loop

    IEnumerator SpawnRoutine()
    {
        while (_continueSpawning)
        {
            Vector3 spawnPosition = new Vector3(Random.Range(-8f, 8f), 7, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform; // parent is a transform type

            yield return new WaitForSeconds(5.0f); // wait for 5 seconds so we dont get a stack overflow
        }
    }

    public void OnPlayerDeath()
    {
        _continueSpawning = false;
    }
}
