using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI scoreTextDisplay;
	private int _score;

	public void RegisterEnemy(Enemy enemy) {
		//todo subscribe to onkilled
	}
	
	private void Start() {
		ResetScore();
	}

	private void OnEnemyKilled(int enemyScoreWorth) {
		_score += enemyScoreWorth;
		UpdateScoreDisplay();
	}

	private void UpdateScoreDisplay() {
		scoreTextDisplay.text = _score.ToString();
	}
	
	private void ResetScore() {
		_score = 0;
	}
}
