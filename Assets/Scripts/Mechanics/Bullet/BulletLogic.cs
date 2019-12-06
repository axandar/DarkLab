using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour {
	private void OnTriggerEnter2D(Collider2D other) {
		if (other.CompareTag(Tags.ENEMY)) {
			Destroy(other.gameObject);
			Destroy(gameObject);
		}
	}
}
