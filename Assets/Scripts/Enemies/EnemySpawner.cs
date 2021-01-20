using System;
using System.Collections;
using System.Collections.Generic;
using Readonly;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : SerializedMonoBehaviour {
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform enemyHolderTransform;
    [SerializeField] private float timeBetweenEnemySpawn;
    [SerializeField] private float portalRadius;
    
    private ScoreManager _scoreManager;

    private void Start() {
        CacheReferences();
        StartCoroutine(SpawnEnemiesCoroutine());
    }

    private void CacheReferences() {
        _scoreManager = GameObject.FindGameObjectWithTag(Tags.SCORE_MANAGER).GetComponent<ScoreManager>();
    }

    private IEnumerator SpawnEnemiesCoroutine(){
        for (;;) {
            var enemy = SpawnEnemyAndReturnReference();
            var enemyScript = enemy.GetComponent<Enemy>();
            
            _scoreManager.RegisterEnemy(enemyScript);
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
        }
    }

    private GameObject SpawnEnemyAndReturnReference() {
        var spawnPosition = (Vector3) Random.insideUnitCircle * portalRadius + gameObject.transform.position ;
        var instantiatedEnemy = Instantiate(GetEnemyToSpawn(),spawnPosition , Quaternion.identity, enemyHolderTransform);
        return instantiatedEnemy;
    }

    private GameObject GetEnemyToSpawn() {
        if (enemyPrefabs == null || enemyPrefabs.Count == 0) {
            throw new ArgumentException("No enemy prefab list provided!");
        }
        return enemyPrefabs[Random.Range(0, enemyPrefabs.Count)];
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(gameObject.transform.position, portalRadius);
    }
}
