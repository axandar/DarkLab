using System;
using Readonly;
using UnityEngine;

public class BulletLogic : MonoBehaviour {
	[SerializeField] private int damageDealt;
	[SerializeField] private int maxTurretDistance;

	private Rigidbody2D _rigidbody2D;
	private Action<BulletLogic> _onBulletReturn;
	private Transform _turretTransform;
	
	private void Awake() {
		_rigidbody2D = gameObject.GetComponent<Rigidbody2D>();
	}

	public void ClearVelocity() {
		_rigidbody2D.velocity = Vector2.zero;
	}

	public void SetupBullet(Transform turretTransform, Action<BulletLogic> onBulletReturn) {
		_onBulletReturn = onBulletReturn;
		_turretTransform = turretTransform;
	}

	private void Update() {
		if (Vector2.Distance(transform.position, _turretTransform.position) >= maxTurretDistance ) {
			_onBulletReturn.Invoke(this);
		}
	}

	private void OnTriggerEnter2D(Collider2D other) {
		if (!other.CompareTag(Tags.ENEMY)) return;
		var enemyScript = other.GetComponent<Enemy>();
		enemyScript.ReceiveDamage(damageDealt);
		
		_onBulletReturn.Invoke(this);
	}
	
	public Rigidbody2D Rigidbody2D => _rigidbody2D;
}
