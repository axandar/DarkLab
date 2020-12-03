using System;
using System.Collections;
using UnityEngine;

public class DarkSoldierBullet : MonoBehaviour {

    [NonSerialized] public Enemy TargetEnemy;
    [NonSerialized] public int BulletDamage;

    [SerializeField] private float bulletLerpTime;

    private Vector3 _startingPosition;

    private void Start() {
        _startingPosition = transform.position;
        StartCoroutine(LerpToTargetCoroutine());
    }

    private IEnumerator LerpToTargetCoroutine() {
        var timeElapsed = 0f;
        while (timeElapsed < bulletLerpTime) {
            if (TargetEnemy == null) {
                Destroy(gameObject);
                yield break;
            }
            var lerpRatio = timeElapsed / bulletLerpTime;
            transform.position = Vector3.Slerp(_startingPosition, TargetEnemy.transform.position, lerpRatio);
            timeElapsed += Time.deltaTime;
            yield return null;
        }
        //TODO: implement object pooling
        TargetEnemy.ReceiveDamage(BulletDamage);
        Destroy(gameObject);
    }
}
