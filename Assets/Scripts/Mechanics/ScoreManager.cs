using TMPro;
using UnityEngine;

public class ScoreManager : MonoBehaviour {
	
	[SerializeField] private TextMeshProUGUI scoreTextDisplay;
	private int _score;
	private bool shouldCountScore;
	
	public void StopCountingAndCacheHighscore() {
		HighscoreCache.highscore = _score;
		shouldCountScore = false;
	}
	
	public void RegisterEnemy(Enemy enemy) {
		enemy.SubscribeToOnDestroyedCallback(OnEnemyKilled);
	}
	
	private void Start() {
		shouldCountScore = true;
		ResetScore();
	}

	private void OnEnemyKilled(int enemyScoreWorth) {
		if (!shouldCountScore) {
			return;
		}
		_score += enemyScoreWorth;
		UpdateScoreDisplay();
	}

	private void UpdateScoreDisplay() {
		scoreTextDisplay.text = _score.ToString();
	}
	
	private void ResetScore() {
		_score = 0;
		UpdateScoreDisplay();
	}
}
