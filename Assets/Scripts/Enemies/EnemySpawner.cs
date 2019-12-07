using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemySpawner : SerializedMonoBehaviour{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private Dictionary<GameObject, float> enemyPrefabDictionary;
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
