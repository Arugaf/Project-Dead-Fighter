using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 1f;
    public float rotationSpeed = 1f;

    private Vector3 _direction;

    void Start() {
        _direction = transform.forward * speed;
        Destroy(gameObject, 5f);
    }

    void Update() {
        transform.Translate(_direction * Time.deltaTime, Space.World);
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }
}
