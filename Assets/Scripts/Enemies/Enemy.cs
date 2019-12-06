using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour{
	private GameObject _turret;
	private Rigidbody2D _rb;
	
	private void Start(){
		_turret = GameObject.FindGameObjectWithTag(Tags.TURRET);
		_rb = gameObject.GetComponent<Rigidbody2D>();
	}

	private IEnumerator MoveToTurret(){
		
	}
	
	
}
