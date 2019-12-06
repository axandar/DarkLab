using System.Collections;
using UnityEngine;

public class BulletDestroy : MonoBehaviour {
	[SerializeField] private float bulletDestroyTime;

	private void Start() {
		StartCoroutine(BulletDestroyCoroutine());
	}

	private IEnumerator BulletDestroyCoroutine() {
		//TODO: implement object pooling
		yield return new WaitForSeconds(bulletDestroyTime);
		Destroy(gameObject);
	}
}
