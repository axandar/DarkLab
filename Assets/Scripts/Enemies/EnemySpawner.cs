using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class EnemySpawner : SerializedMonoBehaviour{
    [SerializeField] private Dictionary<GameObject, float> enemyPrefabDictionary;
    [SerializeField] private Transform enemyHolderTransform;
    [SerializeField] private float timeBetweenEnemySpawn;
    [SerializeField] private float portalRadius;

    private GameController _gameController;
    private void Start() {
        _gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
        StartCoroutine(SpawnEnemy());
    }

    private void DestroySpawner() {
        //TODO: invoke when spawner is destroyed;
        _gameController.PortalClosed();
    }

    private IEnumerator SpawnEnemy(){
        for (;;){
            var spawnPosition = (Vector3) Random.insideUnitCircle * portalRadius + gameObject.transform.position ;
            Instantiate(GetEnemyToSpawn(),spawnPosition , Quaternion.identity, enemyHolderTransform);
            yield return new WaitForSeconds(timeBetweenEnemySpawn);
        }
    }

    private GameObject GetEnemyToSpawn(){
        float total = 0;
        
        foreach (var item in enemyPrefabDictionary) {
            total += item.Value;
        }
        
        float randomPoint = Random.value * total;
        
        foreach (var item in enemyPrefabDictionary){
            if (randomPoint <= item.Value) {
                return item.Key;
            }
            randomPoint -= item.Value;
        }
        return null;
    }
    
    private void OnDrawGizmos() {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(gameObject.transform.position, portalRadius);
    }
    
}
