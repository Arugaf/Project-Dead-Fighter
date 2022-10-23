using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HpManager : MonoBehaviour {
    public UnityEvent onNoHp;
    
    public int hp = 100;

    public void Hit(int amount) {
        hp -= amount;
        
        if (hp <= 0) {
            onNoHp.Invoke();
        }
    }
}
