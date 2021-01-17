using System;
using System.Collections;
using Enemies;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour {
	[SerializeField] private EnemyData enemyData;
	private Action<int> OnEnemyDestroyAction;
	
	private int _currentHealth;
	private GameObject _turret;
	private Transform _transform;
	private Vector3 _toTurretVector;
	private Vector3 _vectorToSecondaryTarget;
	private GameController _gameController;

	public void SubscribeToOnDestroyedCallback(Action<int> callbackAction) {
		OnEnemyDestroyAction += callbackAction;
	}
	
	public void ReceiveDamage(int damageDealt) {
		_currentHealth -= damageDealt;
		Instantiate(enemyData.tinyParticleSystemPrefab, transform.position, Quaternion.identity);
		CheckHealth();
	}

	private void CheckHealth() {
		if (_currentHealth > 0) return;
		_gameController.EnemyDestroyed(enemyData.pointsForEnemy);
		Destroy(gameObject);
	}
	
	private void Start() {
		InitializeFields();
		StartCoroutine(GetRandomSecondaryTarget());
		StartCoroutine(MoveToSecondaryTarget());	
	}

	private void InitializeFields() {
		_gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
		_turret = GameObject.FindGameObjectWithTag(Tags.TURRET);
		_transform = transform;
		_currentHealth = enemyData.maxHealth;
	}

	private void FixedUpdate(){
		_toTurretVector = _turret.transform.position - _transform.position;
		var targetAngle = Mathf.Atan2(_toTurretVector.y, _toTurretVector.x) * Mathf.Rad2Deg;
		_transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
		_transform.position += enemyData.speed * Time.deltaTime * transform.right;
	}

	private IEnumerator MoveToSecondaryTarget(){
		for (;;){
			_transform.position += Time.deltaTime * enemyData.speed * _vectorToSecondaryTarget;
			yield return null;
		}
	}

	private IEnumerator GetRandomSecondaryTarget(){
		for (;;) {
			var randomizedVector = new Vector3(
				Random.Range(enemyData.targetSearchOffsetX * -1, enemyData.targetSearchOffsetX),
				Random.Range(enemyData.targetSearchOffsetY * -1, enemyData.targetSearchOffsetY));
			_vectorToSecondaryTarget = _toTurretVector.normalized + randomizedVector;
			var timeToWait = Random.Range(enemyData.minTimeForNextShake, enemyData.maxTimeForNextShake);
			yield return new WaitForSeconds(timeToWait);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(Tags.LABORATORY)){
			var laboratoryHp = other.GetComponent<LaboratoryHP>();
			laboratoryHp.DecreaseHp(enemyData.dealtDamage);
			Destroy(gameObject);
		}else if(other.CompareTag(Tags.DARK_SOLDIER)){
			var darkSoldierController = other.GetComponent<DarkSoldierController>();
			darkSoldierController.DecreaseHp(enemyData.dealtDamage);
			Destroy(gameObject);
		}
	}

	private void OnDestroy(){
		if (Application.IsPlaying(gameObject)){
			Instantiate(enemyData.particleSystemPrefab, transform.position, Quaternion.identity);
			
			OnEnemyDestroyAction.Invoke(enemyData.pointsForEnemy);
			OnEnemyDestroyAction = null;
		}
	}
}
