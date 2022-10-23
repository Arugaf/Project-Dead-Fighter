using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour {
    public Transform spawnPoint;

    public void Shoot(GameObject objectToShoot) {
        Instantiate(objectToShoot, spawnPoint.position, transform.rotation);
    }
}
