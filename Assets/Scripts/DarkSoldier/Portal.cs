using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private float channelingTime;
	[SerializeField] private GameObject closingPortalParticles;

	private bool _eKeyDown;
	private bool _channelingStarted;
	private GameController _gameController;

	private void Start() {
		_gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
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

	private void OnTriggerStay2D(Collider2D other){
		if (!other.CompareTag(Tags.DARK_SOLDIER)) return;
		if (!_eKeyDown) return;
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
		Instantiate(closingPortalParticles, transform.position, Quaternion.identity);
		yield return new WaitForSeconds(channelingTime);
		_gameController.PortalClosed();
		Destroy(gameObject);
	}
}
