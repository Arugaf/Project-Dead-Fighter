using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHpManager : MonoBehaviour {
    public void EndThis() {
        var rigidBody = GetComponent<Rigidbody>();

        rigidBody.constraints = RigidbodyConstraints.None;
        rigidBody.centerOfMass = new Vector3(0f, 50f, 0f);
        // rigidBody.AddTorque(-transform.forward * 100000f);
        
        GameManager.instance.OnPlayerDeath();
        Debug.Log("Ended");
    }
}
