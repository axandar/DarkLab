using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LaboratoryHP : MonoBehaviour {
	[SerializeField] private int hp;
	[SerializeField] private int maxHp;
	[SerializeField] private int repairAmount;
	[SerializeField] private Image healthBar;
	[SerializeField] private float channelingTime;
	[SerializeField] private GameObject repairBaseParticles;
	
	private GameController _gameController;
	private bool _eKeyDown;
	private bool _channelingStarted;

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

	public void IncreaseHP(int byAmount){
		hp += byAmount;
		if (hp > maxHp){
			hp = maxHp;
			UpdateHpAmountDisplay();
		}else {
			UpdateHpAmountDisplay();
		}
	}
	
	private void Update(){
		if (Input.GetKeyDown(KeyCode.E)){
			_eKeyDown = true;
		}
		if (!Input.GetKeyUp(KeyCode.E)) return;
		_eKeyDown = false;
		_channelingStarted = false;
		StopAllCoroutines();
	}

	private void UpdateHpAmountDisplay() {
		healthBar.fillAmount = (float)hp / maxHp;
	}

	private void OnTriggerStay2D(Collider2D other) {
		if (!other.CompareTag(Tags.DARK_SOLDIER)) {return;}
		if (!_eKeyDown) {return;}
		if (!_channelingStarted){
			StartCoroutine(ChannelPortalClosing());
		}
	}
	
	private void OnTriggerExit2D(Collider2D other){
		if (!other.CompareTag(Tags.DARK_SOLDIER)) return;
		_channelingStarted = false;
		StopAllCoroutines();
	}

	private IEnumerator ChannelPortalClosing(){
		_channelingStarted = true;
		Instantiate(repairBaseParticles, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(channelingTime);
		IncreaseHP(repairAmount);
		_channelingStarted = false;
	}
}
