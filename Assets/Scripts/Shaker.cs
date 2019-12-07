using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class Shaker : MonoBehaviour{
    [SerializeField] private float shakeIntensity;
    private Transform _transform;
    private Vector3 _initialPosition;
    private float _pendingShakeDuration;
    private bool _isShaking;

    private void Start(){
        _transform = GetComponent<Transform>();
        _initialPosition = transform.localPosition;
    }

    public void Shake(float duration){
        if (duration > 0){
            _pendingShakeDuration += duration;
        }
    }

    private void Update(){
        if (_pendingShakeDuration > 0 && !_isShaking){
            StartCoroutine(DoShake());
        }
    }

    private IEnumerator DoShake(){
        _isShaking = true;

        var startTime = Time.realtimeSinceStartup;
        while (Time.realtimeSinceStartup < startTime + _pendingShakeDuration){
            var randomPoint = new Vector3(Random.Range(-1f,1f) * shakeIntensity,
                Random.Range(-1f,1f) * shakeIntensity,_initialPosition.z);
            transform.localPosition = randomPoint;
            yield return null;
        }
        _pendingShakeDuration = 0f;
        _transform.localPosition = _initialPosition;
        _isShaking = false;
    }
}
