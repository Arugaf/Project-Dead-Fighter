using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobShooter : MonoBehaviour {
    public Transform toFollow;

    public float rotationSpeed = 1f;
    
    public GameObject objectToShoot;

    public float cdTime = 1f;

    private Shooter _shooter;

    void Start() {
        _shooter = GetComponent<Shooter>();
        InvokeRepeating(nameof(Shoot), cdTime, cdTime);
    }

    void Update() {
        transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, toFollow.position - transform.position, rotationSpeed * Time.deltaTime, 0.0f));
    }
    

    void Shoot() {
        _shooter.Shoot(objectToShoot);
    }
}
