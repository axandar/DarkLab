using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLogic : MonoBehaviour {
	[SerializeField] private int damageDealt;
	
	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag(Tags.ENEMY)) return;
		var enemyScript = other.GetComponent<Enemy>();
		enemyScript.ReceiveDamage(damageDealt);
		Destroy(gameObject);
	}
}
