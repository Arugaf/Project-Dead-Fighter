using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobShooter : MonoBehaviour {
    public Transform toFollow;

    public float rotationSpeed = 1f;
    
    public GameObject objectToShoot;

    public float cdTime = 1f;

    private Shooter _shooter;

    public float viewDistance = 3f;
    private bool _isVisible;

    public void checkIfVisible() {
        _isVisible = Vector3.Distance(transform.position, toFollow.position) < viewDistance;
    }

    void Start() {
        _shooter = GetComponent<Shooter>();
        InvokeRepeating(nameof(Shoot), cdTime, cdTime);
    }

    void Update() {
        checkIfVisible();

        if (_isVisible) {
            transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, toFollow.position - transform.position, rotationSpeed * Time.deltaTime, 0.0f));
        }
    }
    

    void Shoot() {
        if (_isVisible) {
            _shooter.Shoot(objectToShoot);
        }
    }
}
