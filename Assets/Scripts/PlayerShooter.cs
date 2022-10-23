using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
    public GameObject objectToShoot;

    public Transform spawnPoint;

    public float cdTime = 1f;
    private bool _cooldown = false;

    float AngleAboutY(Transform obj) {
        Vector3 objFwd = obj.forward;
        float angle = Vector3.Angle(objFwd, Vector3.forward);
        float sign = Mathf.Sign(Vector3.Cross(objFwd, Vector3.forward).y);
        return angle * sign;
    }

    void Update() {
        if (!Input.GetMouseButton(0) && !Input.GetKey("r") || _cooldown) return;

        _cooldown = true;
        Invoke(nameof(ResetCooldown), cdTime);
        var projectile = Instantiate(objectToShoot, spawnPoint.position, transform.rotation);
    }

    void ResetCooldown() {
        _cooldown = false;
    }
}
