using System.Collections;
using Readonly;
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
    private Vector2 _portalPrefabColliderSize;

    private Transform _turretTransform;
    private GameController _gameController;

    private void Start() {
	    InitializeFields();
	    StartCoroutine(SpawnPortalsCoroutine());
    }

    private void InitializeFields() {
	    _turretTransform = GameObject.FindGameObjectWithTag(Tags.TURRET).transform;
	    _portalPrefabColliderSize = portalPrefab.GetComponent<BoxCollider2D>().size;
	    _gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
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
		    bool collidingPortals = false;
		    var spawnerPosition = transform.position;
		    var xOffset = Random.Range(-xSpawnOffset, xSpawnOffset);
		    var yOffset = Random.Range(-ySpawnOffset, ySpawnOffset);
		    var spawnPosition = new Vector3(spawnerPosition.x + xOffset, spawnerPosition.y + yOffset);
		    if (Vector3.Distance(_turretTransform.position, spawnPosition) < minimalDistanceToTurret) {
			    continue;
		    }

		    var collidingColliders = Physics2D.OverlapBoxAll(spawnPosition, _portalPrefabColliderSize, 0f);
		    foreach (var collider in collidingColliders) {
			    if (collider.CompareTag(Tags.PORTAL)) {
				    collidingPortals = true;
			    }
		    }
		    if (collidingPortals) {
			    continue;
		    }

		    _gameController.PortalOpened();
		    var instantiatedPortal = Instantiate(portalPrefab, spawnPosition, Quaternion.identity);
		    break;
	    }
    }

    private void OnDrawGizmos() {
	    Gizmos.color = Color.red;
	    Gizmos.DrawWireCube(transform.position, new Vector3(xSpawnOffset * 2, ySpawnOffset * 2));
	    Gizmos.color = Color.green;
	    if (_turretTransform == null) {
		    return;
	    }
	    Gizmos.DrawWireSphere(_turretTransform.position, minimalDistanceToTurret);
    }
    
}
