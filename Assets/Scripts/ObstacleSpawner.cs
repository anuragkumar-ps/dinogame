
using System.Collections;
using System.Collections.Generic;
using UI;
using UnityEngine;
using Random = UnityEngine.Random;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float baseSpawnDuration;
    [SerializeField] private float destroyDelay;
    
    private Coroutine _spawnObstacleCoroutine;

    private void OnGameStart()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        _spawnObstacleCoroutine =  StartCoroutine(SpawnObstacleCoroutine());
    }

    private void OnGameOver()
    {
        StopCoroutine(_spawnObstacleCoroutine);
        
    }

    private void SubscribeEvents()
    {
        StartMenuManager.OnGameStart += OnGameStart;
        GameplayManager.OnGameOver += OnGameOver;
    }

    private void UnSubscribeEvents()
    {
        StartMenuManager.OnGameStart -= OnGameStart;
        GameplayManager.OnGameOver -= OnGameOver;
    }

    private void OnEnable()
    {
        SubscribeEvents();
    }
    private void OnDisable()
    {
        UnSubscribeEvents();
    }
    

    private IEnumerator SpawnObstacleCoroutine()
    {
        while (true)
        {
            var someRandomOffset = Random.Range(-baseSpawnDuration / 2, baseSpawnDuration / 2);
            yield return new WaitForSeconds(baseSpawnDuration + someRandomOffset);
            SpawnObstacle();
        }
    }
    

    private void SpawnObstacle()
    {
        var obstacle = Instantiate(obstaclePrefab, transform);
        Destroy(obstacle, destroyDelay);
    }
}
