using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class LevelCheckpoint : MonoBehaviour {
    Collider2D coll;
    public Transform spawnPoint;
    public GameObject linkedPlayer;
    public LevelCheckpoint pairedCheckpoint;

    // Start is called before the first frame update
    void Start () {
        coll = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update () {

    }
}
