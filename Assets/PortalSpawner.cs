using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

public class PortalSpawner : MonoBehaviour {
	[SerializeField] private GameObject portalPrefab;
	[SerializeField] private int maxAmountOfPortals;
	[SerializeField] private float portalSpawnInterval;
	[SerializeField] private float minimalDistanceToTurret;
	[SerializeField] private float xSpawnOffset;
    [SerializeField] private float ySpawnOffset;

    private int _amountOfPortals;

    private Transform _turretTransform;

    private void Start() {
	    _turretTransform = GameObject.FindGameObjectWithTag(Tags.TURRET).transform;
	    StartCoroutine(SpawnPortalsCoroutine());
    }

    public void OnPortalDestroyed() {
	    _amountOfPortals--;
    }
    
    private IEnumerator SpawnPortalsCoroutine() {
	    for (;;) {
		    if (_amountOfPortals < maxAmountOfPortals) {
			    SpawnPortal();
		    }
		    yield return new WaitForSeconds(portalSpawnInterval);
	    }
    }

    private void SpawnPortal() {
	    _amountOfPortals++;
	    for (;;) {
		    var spawnerPosition = transform.position;
		    var xOffset = Random.Range(-xSpawnOffset, xSpawnOffset);
		    var yOffset = Random.Range(-ySpawnOffset, ySpawnOffset);
		    var spawnPosition = new Vector3(spawnerPosition.x + xOffset, spawnerPosition.y + yOffset);
		    if (Vector3.Distance(_turretTransform.position, spawnPosition) < minimalDistanceToTurret) {
			    continue;
		    }

		    var instantiatedPortal = Instantiate(portalPrefab, spawnPosition, Quaternion.identity);
		    break;
	    }
    }

    private void OnDrawGizmos() {
	    Gizmos.color = Color.red;
	    Gizmos.DrawWireCube(transform.position, new Vector3(xSpawnOffset * 2, ySpawnOffset * 2));
	    Gizmos.color = Color.green;
	    Gizmos.DrawWireSphere(_turretTransform.position, minimalDistanceToTurret);
    }

    private void OnValidate() {
	    _turretTransform = GameObject.FindGameObjectWithTag(Tags.TURRET).transform;
    }
}
