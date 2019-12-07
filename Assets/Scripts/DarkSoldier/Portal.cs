using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
	[SerializeField] private float channelingTime;

	private bool _eKeyDown;
	private bool _channelingStarted;
	
	private void Update(){
		if (Input.GetKeyDown(KeyCode.E)){
			_eKeyDown = true;
		}

		if (!Input.GetKeyUp(KeyCode.E)) return;
		Debug.Log("E key up!!!");
		_eKeyDown = false;
		_channelingStarted = false;
		StopAllCoroutines();
	}

	private void OnTriggerStay2D(Collider2D other){
		if (!other.CompareTag(Tags.DARK_SOLDIER)) return;
		Debug.Log("PortalTouched");
		if (!_eKeyDown) return;
		if (!_channelingStarted){
			StartCoroutine(ChannelPortalClosing());
		}
	}

	private void OnTriggerExit2D(Collider2D other){
		if (!other.CompareTag(Tags.DARK_SOLDIER)) return;
		Debug.Log("TriggerExited");
		_channelingStarted = false;
		StopAllCoroutines();
	}

	private IEnumerator ChannelPortalClosing(){
		_channelingStarted = true;
		Debug.Log("CoroutineStarted");
		yield return new WaitForSeconds(channelingTime);
		Destroy(gameObject);
	}
}
