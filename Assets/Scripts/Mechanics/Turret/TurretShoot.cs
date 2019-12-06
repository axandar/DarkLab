using System.Collections;
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
            InstantiateBullet();
            yield return new WaitForSeconds(shootInterval);
        }
    }

    private void InstantiateBullet() {
        var bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        var shootVector = transform.right;
        bullet.GetComponent<Rigidbody2D>().AddForce(bulletSpeed * shootVector,ForceMode2D.Impulse);
    }
}
