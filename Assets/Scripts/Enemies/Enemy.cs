using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour{
	[SerializeField] private float speed;
	[SerializeField] private float minTimeForNextShake;
	[SerializeField] private float maxTimeForNextShake;
	[SerializeField] private float offsetX;
	[SerializeField] private float offsetY;
	
	private GameObject _turret;
	private Transform _transform;
	private Vector3 _toTurretVector;
	private Vector3 _vectorToSecondaryTarget;
	
	private void Start(){
		_turret = GameObject.FindGameObjectWithTag(Tags.TURRET);
		_transform = transform;
		StartCoroutine(GetRandomSecondaryTarget());
		StartCoroutine(MoveToSecondaryTarget());
		
	}

	private void FixedUpdate(){
		_toTurretVector = _turret.transform.position - _transform.position;
		var targetAngle = Mathf.Atan2(_toTurretVector.y, _toTurretVector.x) * Mathf.Rad2Deg;
		_transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
		_transform.position += speed * Time.deltaTime * transform.right;
	}

	private IEnumerator MoveToSecondaryTarget(){
		for (;;){
			_transform.position += Time.deltaTime * speed * _vectorToSecondaryTarget;
			yield return null;
		}
	}

	private IEnumerator GetRandomSecondaryTarget(){
		for (;;){
			_vectorToSecondaryTarget = _toTurretVector.normalized + new Vector3(Random.Range(-offsetX, offsetX),
				                              Random.Range(-offsetY, offsetY));
			var timeToWait = Random.Range(minTimeForNextShake, maxTimeForNextShake);
			yield return new WaitForSeconds(timeToWait);
		}
	}
	
	
}
