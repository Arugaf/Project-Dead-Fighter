using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
    public GameObject objectToShoot;

    public float cdTime = 1f;
    private bool _cooldown = false;

    private Shooter _shooter;

    private void Start() {
        _shooter = GetComponent<Shooter>();
    }

    void Update() {
        if (!Input.GetMouseButton(0) && !Input.GetKey("r") || _cooldown) return;

        _cooldown = true;
        Invoke(nameof(ResetCooldown), cdTime);
        _shooter.Shoot(objectToShoot);
    }

    void ResetCooldown() {
        _cooldown = false;
    }
}
