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
		turretShoot.Play();
	}

	public void DsShootSound() {
		DsShoot.Play();
	}

	public void PortalOpenSound() {
		portalOpen.Play();
	}

	public void PortalCloseSound() {
		portalClose.Play();
	}

	public void DsDamageSound() {
		DsDamage.Play();
	}

	public void DsDiedSound() {
		DsDie.Play();
	}

	public void DsRespawnedSound() {
		DsRespawn.Play();
	}

	public void LabDamagedSound() {
		labDamage.Play();
	}

	public void GameOverSound() {
		gameOver.Play();
	}

	public void EnemyDiedSound() {
		enemyDied.Play();
	}
}
