using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobHpManager : MonoBehaviour {
    public void EndThis() {
        MobCounter.MobDied();
        Destroy(gameObject);
    }
}
