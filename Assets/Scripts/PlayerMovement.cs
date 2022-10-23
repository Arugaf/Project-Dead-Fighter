using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    public CharacterController controller;

    public Transform followingCamera;

    public float speed = 6f;

    public float turnSmoothTime = 0.1f;
    public float turnSmoothVelocity;

    void Update() {
        var horizontal = Input.GetAxisRaw("Horizontal");
        var vertical = Input.GetAxisRaw("Vertical");

        var direction = new Vector3(horizontal, 0f, vertical).normalized;
        var isometricDirection = Matrix4x4.Rotate(Quaternion.Euler(0, 45, 0)).MultiplyPoint3x4(direction);

        if (direction.magnitude >= 0.1f) {
            var targetAngle = Mathf.Atan2(isometricDirection.x, isometricDirection.z) * Mathf.Rad2Deg;
            var angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity,
                turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            controller.Move(isometricDirection * speed * Time.deltaTime);
        }

        followingCamera.position = transform.position;
    }
}
