using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class MobMovementController : MonoBehaviour {
    public NavMeshAgent agent;

    public Transform destination;

    void Update() {
        agent.SetDestination(destination.position);
    }
}
