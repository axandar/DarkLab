using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections.LowLevel.Unsafe;
using UnityEngine;

public class TurretControl : MonoBehaviour {
    private Camera _mainCamera;

    private void Start() {
        _mainCamera = GameObject.FindGameObjectWithTag(Tags.MAIN_CAMERA).GetComponent<Camera>();
    }

    void Update() {
        var toMouseVector = Input.mousePosition - _mainCamera.WorldToScreenPoint(transform.position);
        var targetAngle = Mathf.Atan2(toMouseVector.y, toMouseVector.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(targetAngle, Vector3.forward); 
    }
}
