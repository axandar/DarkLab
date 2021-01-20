using TMPro;
using UnityEngine;

public class LoseSceneHighscoreDisplay : MonoBehaviour {
	[SerializeField] private TextMeshProUGUI highscoreDisplay;

	private void Start() {
		var highscore = HighscoreCache.highscore;
		highscoreDisplay.text = "Highscore: " + highscore;
	}
}
