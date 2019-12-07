using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	[SerializeField] private UnityEvent gameEndEvent;
	[SerializeField] private UnityEvent enemyDestroyedEvent;

	private int _points;
	public void GameEnded() {
		gameEndEvent.Invoke();
	}

	public void EnemyDestroyed(int pointsForEnemy){
		enemyDestroyedEvent.Invoke();
		_points += pointsForEnemy;
		Debug.Log(_points);
	}
}
