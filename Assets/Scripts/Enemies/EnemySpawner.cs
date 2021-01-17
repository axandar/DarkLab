using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : SerializedMonoBehaviour {
    [SerializeField] private List<GameObject> enemyPrefabs;
    [SerializeField] private Transform enemyHolderTransform;
    [SerializeField] private float timeBetweenEnemySpawn;
    [SerializeField] private float portalRadius;

    private void Start() {
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy(){
        for (;;){
            var spawnPosition = (Vector3) Random.insideUnitCircle * portalRadius + gameObject.transform.position ;
            Instantiate(GetEnemyToSpawn(),spawnPosition , Quaternion.identity, enemyHolderTransform);
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
        }
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
