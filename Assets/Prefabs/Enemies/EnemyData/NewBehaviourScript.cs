using UnityEngine;

public class EnemyData : ScriptableObject {
	public int maxHealth;
	public float speed;
	public float minTimeForNextShake;
	public float maxTimeForNextShake;
	public float targetSearchOffsetX;
	public float targetSearchOffsetY;
	public int pointsForEnemy;
	public int dealtDamage;
	public GameObject particleSystemPrefab;
	public GameObject tinyParticleSystemPrefab;
}
