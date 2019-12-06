using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Transform enemyHolderTransform;
    [SerializeField] private float timeBetweenEnemySpawn;
    
    private void Start(){
        StartCoroutine(SpawnEnemy());
    }

    private IEnumerator SpawnEnemy(){
        for (;;){
            Instantiate(enemyPrefab, gameObject.transform.position, Quaternion.identity, enemyHolderTransform);
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
        }
    }
    
}
