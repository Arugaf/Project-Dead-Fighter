using System;
using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using UnityEngine;
using Matrix4x4 = UnityEngine.Matrix4x4;
using Plane = UnityEngine.Plane;
using Quaternion = UnityEngine.Quaternion;
using Vector3 = UnityEngine.Vector3;

public class PlayerMovement : MonoBehaviour {
    new private Rigidbody rigidbody;
    private CapsuleCollider capsuleCollider;

    public Transform followingCamera;
    private Camera _camera;

    public float moveSpeed = 5f;
    public float moveSmooth = 0.2f;
    public float dashPower = 100f;
    public float dashCooldown = 3f;

    private bool _dashCooldown = false;

    private Vector3 _currentVelocity = Vector3.zero;

    private Plane _plane = new Plane(Vector3.up, new Vector3(0.0f, 1.06f, 0.0f));

    private void Awake() {
        rigidbody = GetComponent<Rigidbody>();
        capsuleCollider = GetComponent<CapsuleCollider>();
        _camera = Camera.main;
    }

    private void Update() {
        var cameraRay = _camera.ScreenPointToRay(Input.mousePosition);
        Debug.DrawRay(cameraRay.origin, cameraRay.direction * 100f, Color.red);

        var distance = 0.0f;
        _plane.Raycast(cameraRay, out distance);
        var hitPoint = cameraRay.GetPoint(distance);
        Debug.DrawRay(transform.position, hitPoint, Color.cyan);

        // var playerPositionOnPlane = _plane.ClosestPointOnPlane(transform.position);
        var lookAtPoint = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
        transform.LookAt(lookAtPoint);
        Debug.DrawRay(transform.position, lookAtPoint, Color.green);
        Debug.DrawLine(new Vector3(transform.position.x, 1.06f, transform.position.z),
            new Vector3(hitPoint.x, 1.06f, hitPoint.z), Color.magenta);
        
        if (Input.GetKeyDown("space") && !_dashCooldown) {
            _dashCooldown = true;
            rigidbody.AddForce(CalculateIsometricDirection() * moveSpeed * dashPower);
            Invoke(nameof(resetDashCooldown), dashCooldown);
        }

        // RaycastHit hit;
        /*if (Physics.Raycast(cameraRay.origin, cameraRay.direction, out var hit, Mathf.Infinity)) {
            transform.LookAt(new Vector3(hit.point.x, transform.position.y, hit.point.z));
            Debug.DrawRay(transform.position, new Vector3(hit.point.x, transform.position.y, hit.point.z), Color.yellow);
        }
        else {
            transform.LookAt(lookAtPoint);
        }*/


        // transform.LookAt(new Vector3(pointToLookAt.x, transform.position.y, pointToLookAt.z));
        // Debug.DrawLine(transform.position, new Vector3(pointToLookAt.x, transform.position.y, pointToLookAt.z), Color.green);
        // Debug.DrawRay(transform.position, new Vector3(pointToLookAt.x, transform.position.y, pointToLookAt.z), Color.cyan);
    }

    private void resetDashCooldown() {
        _dashCooldown = false;
    }

    private void FixedUpdate() {
        var isometricDirection = CalculateIsometricDirection();

        // if (GameManager.instance.CurrentGameStatus != GameManager.GameStatus.NotStarted) {
        rigidbody.velocity = Vector3.SmoothDamp(rigidbody.velocity, isometricDirection * moveSpeed,
            ref _currentVelocity, moveSmooth /* * Time.deltaTime*/);
        // }

        followingCamera.position = transform.position;
    }

    private Vector3 CalculateIsometricDirection() {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var direction = new Vector3(horizontal, 0f, vertical).normalized;
        return Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0)).MultiplyPoint3x4(direction).normalized;
    }
}
