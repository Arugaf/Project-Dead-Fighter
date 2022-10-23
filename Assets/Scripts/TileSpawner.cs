using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileSpawner : MonoBehaviour {
    public GameObject toSpawn;

    public Transform centerOfSpawn;
    public Vector3 spawnSize;
    public uint spawnArea = 1;

    private HashSet<Vector2> _tiles = new HashSet<Vector2>();

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() {
        var spawnPosition = centerOfSpawn.position;
        var zIndex = (int) (spawnPosition.z / (spawnSize.z / 2f));
        var xIndex = (int) (spawnPosition.x / (spawnSize.x / 2f));

        for (var x = -(int) spawnArea; x <= spawnArea; ++x) {
            for (var z = -(int) spawnArea; z <= spawnArea; ++z) {
                if (!_tiles.Contains(new Vector2(xIndex + x, zIndex + z))) {
                    Instantiate(toSpawn,
                        new Vector3((xIndex + x) * spawnSize.x, toSpawn.transform.position.y,
                            (zIndex + z) * spawnSize.z),
                        toSpawn.transform.rotation);
                    _tiles.Add(new Vector2(xIndex + x, zIndex + z));
                }
            }
        }
    }
}
