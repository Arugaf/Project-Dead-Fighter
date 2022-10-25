using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerShooter : MonoBehaviour {
    public GameObject objectToShoot;
    public int inventorySize = 5;

    public List<GameObject> inventory;

    public float cdTime = 1f;
    private bool _cooldown = false;

    private Shooter _shooter;

    private void Start() {
        GameManager.instance.maxInventory = inventorySize;
        GameManager.instance.SetInventory(inventorySize);
    }

    public bool addToInventory(GameObject item) {
        if (inventory.Count < inventorySize) {
            inventory.Add(item);
            GameManager.instance.SetInventory(inventory.Count);
            return true;
        }

        return false;
    }

    private void Awake() {
        _shooter = GetComponent<Shooter>();
        inventory = new List<GameObject>(inventorySize);
        inventory.AddRange(Enumerable.Repeat<GameObject>(objectToShoot, inventorySize));
    }

    void Update() {
        if (!Input.GetMouseButton(0) && !Input.GetKey("r") || _cooldown ||
            GameManager.instance.CurrentGameStatus == GameManager.GameStatus.Paused) return;

        _cooldown = true;
        Invoke(nameof(ResetCooldown), cdTime);
        if (inventory.Count > 0) {
            _shooter.Shoot(inventory.Last());
            inventory.Remove(inventory.Last());
            GameManager.instance.SetInventory(inventory.Count);
        }
    }

    void ResetCooldown() {
        _cooldown = false;
    }
}
