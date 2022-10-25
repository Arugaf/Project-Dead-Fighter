using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HpManager : MonoBehaviour {
    public UnityEvent onNoHp;
    
    public int hp = 100;

    public void Start() {
        // bad
        GameManager.instance.LoadUI();
        if (GetComponent<PlayerHpManager>()) {
            GameManager.instance.maxHp = hp;
            GameManager.instance.SetHp(hp);
        }
    }

    public void Hit(int amount) {
        hp -= amount;
        
        if (hp <= 0) {
            onNoHp.Invoke();
        }
        
        // bad
        if (GetComponent<PlayerHpManager>()) {
            GameManager.instance.SetHp(hp);
        }
    }
}
