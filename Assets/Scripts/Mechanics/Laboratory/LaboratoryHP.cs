using TMPro;
using UnityEngine;

public class LaboratoryHP : MonoBehaviour {
	[SerializeField] private int hp;
	[SerializeField] private TextMeshProUGUI hpDisplayText;
	
	private GameController _gameController;

	private void Start() {
		_gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
		UpdateHpAmountDisplay(hp);
	}

	public void DecreaseHp(int byAmount) {
		_gameController.LaboratoryDamaged();
		hp -= byAmount;
		if (hp <= 0) {
			UpdateHpAmountDisplay(0);
			_gameController.GameEnded();
		} else {
			UpdateHpAmountDisplay(hp);
		}
	}

	private void UpdateHpAmountDisplay(int hpAmount) {
		hpDisplayText.text = "Health: " + hpAmount;
	}
}
