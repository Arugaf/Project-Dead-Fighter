using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobMovementController : MonoBehaviour {
    public NavMeshAgent agent;

    public Transform destination;

    public float viewDistance = 3f;

    private BoxCollider _collider;

    public float distance;

    public float colliderRadius;

    void Awake() {
        _collider = GetComponent<BoxCollider>();
        colliderRadius = Vector3.Distance(_collider.size, new Vector3(0, _collider.size.y, 0)) + 2.5f;
    }

    void Update() {
        distance = Vector3.Distance(transform.position, destination.position);
        if (distance < viewDistance && distance > colliderRadius) {
            agent.SetDestination(destination.position);
        }
        else {
            agent.SetDestination(transform.position);
        }
    }
}
