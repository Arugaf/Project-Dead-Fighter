using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobCounter : MonoBehaviour {
    [SerializeField] private static int MobCounterV;

    private void Awake() {
        MobCounterV = transform.childCount;
    }

    public static void MobDied() {
        if (--MobCounterV == 0) {
            GameManager.instance.OnWin();
        }
    }
}
