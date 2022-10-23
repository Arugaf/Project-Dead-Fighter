using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour {
    public float speed = 1f;
    public float rotationSpeed = 1f;

    public int damage = 25;
    public float destroyTime = 10f;

    private Vector3 _direction;

    private Rigidbody _rigidbody;

    public bool freezeY = true;
    
    public void Launch() {
        enabled = true;
            
        _direction = transform.forward * speed;
        _rigidbody = GetComponent<Rigidbody>();
        _rigidbody.constraints = RigidbodyConstraints.FreezePositionY;

        _rigidbody.velocity = _direction;

        Invoke(nameof(Annihilate), destroyTime);
    }

    void Annihilate() {
        Destroy(gameObject);
    }

    void Update() {
        transform.Rotate(rotationSpeed * Time.deltaTime, 0, 0);
    }

    private void OnCollisionEnter(Collision collision) {
        if (enabled) {
            if (collision.gameObject.GetComponent<Projectile>()) {
                enabled = false;
                _rigidbody.constraints = RigidbodyConstraints.None;
                return;
            }
            
            if (!freezeY) {
                _rigidbody.constraints = RigidbodyConstraints.None;
            }
            else {
                _rigidbody.constraints = RigidbodyConstraints.FreezeAll;
            }

            var receiver = collision.gameObject.GetComponent<HpManager>();
            if (receiver) {
                receiver.Hit(damage);
                Destroy(gameObject);
            }

            enabled = false;
        }
        else {
            var receiver = collision.gameObject.GetComponent<PlayerShooter>();

            if (receiver && receiver.addToInventory(gameObject)) {
                gameObject.SetActive(false);
                CancelInvoke(nameof(Annihilate));
            }
        }
    }
}
