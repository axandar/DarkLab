using UnityEditor.PackageManager;
using UnityEngine;
using UnityEngine.Events;

public class GameController : MonoBehaviour {
	[SerializeField] private UnityEvent GameEndEvent;
	[SerializeField] private UnityEvent EnemyDestroyedEvent;

	private int _points;
	public void GameEnded() {
		GameEndEvent.Invoke();
	}

	public void EnemyDestroyed(int pointsForEnemy){
		EnemyDestroyedEvent.Invoke();
		_points += pointsForEnemy;
		Debug.Log(_points);
	}
}
