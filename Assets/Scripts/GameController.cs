using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	[SerializeField] private UnityEvent gameEndEvent;
	[SerializeField] private UnityEvent turretShotEvent;
	[SerializeField] private UnityEvent darkSoldierShotEvent;
	[SerializeField] private UnityEvent laboratoryDamagedEvent;
	[SerializeField] private UnityEvent enemyDestroyedEvent;
	[SerializeField] private UnityEvent darkSoldierDiedEvent;
	[SerializeField] private UnityEvent portalOpenedEvent;
	[SerializeField] private UnityEvent portalClosedEvent;
	[SerializeField] private GameObject darkSoldierPrefab;
	[SerializeField] private Transform darkSoldierRespawnPosition;
	[SerializeField] private float timeForRespawn;
	private int _points;
	
	public void GameEnded() {
		gameEndEvent.Invoke();
	}

	public void DarkSoldierShot() {
		darkSoldierShotEvent.Invoke();
	}
	
	public void TurretShotABullet() {
		turretShotEvent.Invoke();
	}
	
	public void LaboratoryDamaged() {
		laboratoryDamagedEvent.Invoke();
	}

	public void EnemyDestroyed(int pointsForEnemy){
		enemyDestroyedEvent.Invoke();
		_points += pointsForEnemy;
	}

	public void PortalOpened() {
		portalOpenedEvent.Invoke();
	}

	public void PortalClosed() {
		portalClosedEvent.Invoke();
	}

	public void DarkSoldierDied(){
		Debug.Log("DarkSoldierDied");
		darkSoldierDiedEvent.Invoke();
		StartCoroutine(RiseLikeAPhoenix());
	}

	private IEnumerator RiseLikeAPhoenix(){
		yield return new WaitForSeconds(timeForRespawn);
		Instantiate(darkSoldierPrefab, darkSoldierRespawnPosition.position, Quaternion.identity);
	}
}
