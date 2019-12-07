using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarkSoldierShooting : MonoBehaviour {
    [SerializeField] private float shootingRange;
    [SerializeField] private int shootingDamage;
    [SerializeField] private float shootingInterval;
    [SerializeField] GameObject bulletPrefab;

    private bool _attackCoroutineRunning;
    private Enemy _currentTarget;

    private GameController _gameController;

    private void Start() {
        _gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
    }

    private void FixedUpdate() {
        var enemiesList = ScanForEnemies();
        if (enemiesList.Count == 0) { 
            StopAllCoroutines();
            _attackCoroutineRunning = false;
            return; }
        var enemyToAttack = GetNearestEnemy(enemiesList);
        _currentTarget = enemyToAttack;
        if (!_attackCoroutineRunning) {
            StartCoroutine(AttackEnemiesCoroutine());
        }
    }

    private List<Enemy> ScanForEnemies() {
        var enemyList = new List<Enemy>();
        var collidersInRange = Physics2D.OverlapCircleAll(transform.position, shootingRange);
        foreach (var collider in collidersInRange) {
            if (collider.gameObject.CompareTag(Tags.ENEMY)) {
                enemyList.Add(collider.GetComponent<Enemy>());
            }
        }
        return enemyList;
    }

    private Enemy GetNearestEnemy(List<Enemy> enemiesInRange) {
        Enemy closestEnemy = enemiesInRange[0];
        float closestEnemyDistance = Vector3.Distance(enemiesInRange[0].transform.position, transform.position);
        foreach (var enemy in enemiesInRange) {
            var distanceToEnemy = Vector3.Distance(enemy.transform.position, transform.position);
            if (distanceToEnemy  < closestEnemyDistance) {
                closestEnemy = enemy;
                closestEnemyDistance = distanceToEnemy;
            }
        }
        return closestEnemy;
    }

    private IEnumerator AttackEnemiesCoroutine() {
        _attackCoroutineRunning = true;
        for (;;) {
            FireAtEnemy(_currentTarget);
            yield return new WaitForSeconds(shootingInterval);
        }
    }

    private void FireAtEnemy(Enemy enemy) {
        var bulletScript = Instantiate(bulletPrefab, transform.position, Quaternion.identity).GetComponent<DarkSoldierBullet>();
        bulletScript.BulletDamage = shootingDamage;
        bulletScript.TargetEnemy = _currentTarget;
        _gameController.DarkSoldierShot();
    }
}
