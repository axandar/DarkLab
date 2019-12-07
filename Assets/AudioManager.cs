using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class AudioManager : MonoBehaviour {
	[SerializeField] private AudioSource turretShoot,
		DsShoot,
		portalOpen,
		portalClose,
		DsDamage,
		DsDie,
		DsRespawn,
		labDamage,
		gameOver,
		enemyDied;

	public void TurretShootSound() {
		turretShoot.PlayOneShot(turretShoot.clip);
	}

	public void DsShootSound() {
		DsShoot.PlayOneShot(DsShoot.clip);
	}

	public void PortalOpenSound() {
		portalOpen.PlayOneShot(portalOpen.clip);
	}

	public void PortalCloseSound() {
		portalClose.PlayOneShot(portalClose.clip);
	}

	public void DsDamageSound() {
		DsDamage.PlayOneShot(DsDamage.clip);
	}

	public void DsDiedSound() {
		DsDie.PlayOneShot(DsDie.clip);
	}

	public void DsRespawnedSound() {
		DsRespawn.PlayOneShot(DsRespawn.clip);
	}

	public void LabDamagedSound() {
		labDamage.PlayOneShot(labDamage.clip);
	}

	public void GameOverSound() {
		gameOver.PlayOneShot(gameOver.clip);
	}

	public void EnemyDiedSound() {
		enemyDied.PlayOneShot(enemyDied.clip);
	}
}
