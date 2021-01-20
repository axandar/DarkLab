using System.Collections;
using ObjectPool;
using Readonly;
using UnityEngine;

public class TurretShoot : MonoBehaviour {
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;
    [SerializeField] private float shootInterval;
    [SerializeField] private float bulletSpeed;
    private bool _mousePressed;
    private bool _mousePressedLastFrame;
    private bool _shootCooldown;
    private bool _shootBulletsCoroutineStarted;

    private GameController _gameController;
    private GameObjectPool<BulletLogic> _bulletPool;

    private void Start() {
        InitializeFields();
    }

    private void InitializeFields() {
        _gameController = GameObject.FindGameObjectWithTag(Tags.GAME_CONTROLLER).GetComponent<GameController>();
        _bulletPool = new GameObjectPool<BulletLogic>(10, bulletPrefab, BulletInstantiated, null, null);
    }
    
    private void BulletInstantiated(BulletLogic bulletLogic) {
        bulletLogic.SetupBullet(transform, ReturnBulletToPool);
    }

    private void ReturnBulletToPool(BulletLogic bulletLogic) {
        _bulletPool.ReturnObject(bulletLogic);
    }

    private void Update() {
        _mousePressed = Input.GetKey(KeyCode.Mouse0);
        if (_shootCooldown) {
            _mousePressedLastFrame = _mousePressed;
            return;
        }
        if (_mousePressed && !_mousePressedLastFrame) {
            StartCoroutine(ShootBulletsCoroutine());
        }
        if (_mousePressed && _mousePressedLastFrame && !_shootBulletsCoroutineStarted) {
           StartCoroutine(ShootBulletsCoroutine()); 
        }
        if (!_mousePressed && _mousePressedLastFrame) {
            StopAllCoroutines();
            _shootBulletsCoroutineStarted = false;
            StartCoroutine(ShootCooldownCoroutine());
        }
        _mousePressedLastFrame = _mousePressed;
    }

    private IEnumerator ShootCooldownCoroutine() {
        _shootCooldown = true;
        yield return new WaitForSeconds(shootInterval);
        _shootCooldown = false;
    }

    private IEnumerator ShootBulletsCoroutine() {
        _shootBulletsCoroutineStarted = true;
        for (;;) {
            ShootBullet();
            yield return new WaitForSeconds(shootInterval);
        }
    }
    
    private void ShootBullet() {
        var bullet = _bulletPool.GetObject();
        bullet.ClearVelocity();
        
        bullet.transform.position = bulletSpawnPoint.transform.position;
        var shootVector = transform.right;
        bullet.Rigidbody2D.AddForce(bulletSpeed * shootVector,ForceMode2D.Impulse);
        _gameController.TurretShotABullet();
    }
}
