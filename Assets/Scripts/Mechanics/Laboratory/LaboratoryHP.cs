using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LaboratoryHP : MonoBehaviour {
	[SerializeField] private int hp;
	[SerializeField] private int maxHp;
	[SerializeField] private Image healthBar;
	
	private GameController _gameController;

	private void Start() {
		_gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
		UpdateHpAmountDisplay();
	}

	public void DecreaseHp(int byAmount) {
		_gameController.LaboratoryDamaged();
		hp -= byAmount;
		if (hp <= 0) {
			UpdateHpAmountDisplay();
			_gameController.GameEnded();
		} else {
			UpdateHpAmountDisplay();
		}
	}

	private void UpdateHpAmountDisplay() {
		healthBar.fillAmount = (float)hp / maxHp;
		
	}
}
